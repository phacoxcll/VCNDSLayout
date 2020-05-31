using System;
using System.Collections;
using System.IO;
using System.Text;

namespace JSON
{
    public class LexicalAnalyzer
    {
        private StreamReader Source;
        private char Lookahead;
        private Hashtable Words;

        public LexicalAnalyzer(StreamReader source)
        {
            Lookahead = ' ';
            Words = new Hashtable();

            Words.Add(Type.Null.Lexeme, Type.Null);
            Words.Add(Word.False.Lexeme, Word.False);
            Words.Add(Word.True.Lexeme, Word.True);

            Source = source;
        }

        private void Read()
        {
            if (!Source.EndOfStream)
                Lookahead = Convert.ToChar(Source.Read());
            else
                Lookahead = '\0';
        }

        public Token GetNextToken()
        {
            for (; ; Read())
            {
                if (Lookahead == '\u0009' || //Horizontal tap
                    Lookahead == '\u000A' || //Linefeed
                    Lookahead == '\u000D' || //Carriage return
                    Lookahead == '\u0020')   //Space
                    continue;
                else
                    break;
            }

            if (char.IsLetter(Lookahead))
                return WordToken();

            if (Lookahead == '"')
                return StringToken();

            if (char.IsDigit(Lookahead) || Lookahead == '-')
                return NumberToken();

            Token token = new Token(Lookahead);
            Lookahead = ' ';
            return token;
        }

        private Token WordToken()
        {
            StringBuilder strBuilder = new StringBuilder();

            for (; ; Read())
            {
                if (char.IsLetterOrDigit(Lookahead))
                    strBuilder.Append(Lookahead);
                else
                    break;
            }

            string str = strBuilder.ToString();
            Word word = (Word)Words[str];

            if (word != null)
                return word;
            else
                throw new Exception("\"" + word.Lexeme + "\" is not a reserved word.");
        }

        private Token StringToken()
        {
            StringBuilder strBuilder = new StringBuilder();

            for (; ; )
            {
                Read();
                if (char.IsControl(Lookahead))
                    throw new Exception("Control character 0x" + ((byte)Lookahead).ToString("X8") + " detected within string.");
                else if (Lookahead == '\\')
                {
                    Read();
                    switch (Lookahead)
                    {
                        case '"':
                            strBuilder.Append("\"");
                            break;
                        case '\\':
                            strBuilder.Append("\\");
                            break;
                        case '/':
                            strBuilder.Append("/");
                            break;
                        case 'b':
                            strBuilder.Append("\b");
                            break;
                        case 'f':
                            strBuilder.Append("\f");
                            break;
                        case 'n':
                            strBuilder.Append("\n");
                            break;
                        case 'r':
                            strBuilder.Append("\r");
                            break;
                        case 't':
                            strBuilder.Append("\t");
                            break;
                        case 'u':
                            strBuilder.Append(FromHex(FourHex()));
                            break;
                        default:
                            throw new Exception("Invalid escape code 0x" + ((byte)Lookahead).ToString("X8") + ".");
                    }
                }
                else if (Lookahead != '"')
                    strBuilder.Append(Lookahead);
                else if (Lookahead == '"')
                {
                    Lookahead = ' ';
                    break;
                }
                else
                    throw new Exception("Invalid character 0x" + ((byte)Lookahead).ToString("X8") + ".");
            }

            return new StringToken(strBuilder.ToString(), WordLabel.String);
        }

        private Token NumberToken()
        {
            StringBuilder strBuilder = new StringBuilder();

            //Integer
            if (Lookahead == '-')
            {
                strBuilder.Append(Lookahead);
                Read();
            }

            if (Lookahead == '0')
            {
                strBuilder.Append(Lookahead);
                Read();
            }
            else if (IsOnenine(Lookahead))
            {
                strBuilder.Append(Lookahead);
                Read();

                for (; ; Read())
                {
                    if (char.IsDigit(Lookahead))
                        strBuilder.Append(Lookahead);
                    else
                        break;
                }
            }
            else
                throw new Exception("No is a number 0x" + ((byte)Lookahead).ToString("X8") + ".");

            //Fraction
            if (Lookahead == '.')
            {
                strBuilder.Append(Lookahead);
                Read();

                if (!char.IsDigit(Lookahead))
                    throw new Exception("No is a number 0x" + ((byte)Lookahead).ToString("X8") + ".");

                for (; ; Read())
                {
                    if (char.IsDigit(Lookahead))
                        strBuilder.Append(Lookahead);
                    else
                        break;
                }
            }

            //Exponent
            if (Lookahead == 'e' || Lookahead == 'E')
            {
                strBuilder.Append("E");
                Read();

                if (Lookahead == '+' || Lookahead == '-')
                {
                    strBuilder.Append(Lookahead);
                    Read();
                }

                if (!char.IsDigit(Lookahead))
                    throw new Exception("No is a number 0x" + ((byte)Lookahead).ToString("X8") + ".");

                for (; ; Read())
                {
                    if (char.IsDigit(Lookahead))
                        strBuilder.Append(Lookahead);
                    else
                        break;
                }
            }

            string number = strBuilder.ToString();
            return new NumberToken(Convert.ToDouble(number), WordLabel.Number);
        }


        private string FourHex()
        {
            Read();
            if (IsHex(Lookahead))
            {
                string s = Lookahead.ToString();
                Read();
                if (IsHex(Lookahead))
                {
                    s += Lookahead.ToString();
                    Read();
                    if (IsHex(Lookahead))
                    {
                        s += Lookahead.ToString();
                        Read();
                        if (IsHex(Lookahead))
                        {
                            s += Lookahead.ToString();
                            return s;
                        }
                    }
                }
            }
            throw new Exception("Does not have four hexadecimal numbers.");
        }


        private bool IsOnenine(char c)
        {
            return (
                c == '1' ||
                c == '2' ||
                c == '3' ||
                c == '4' ||
                c == '5' ||
                c == '6' ||
                c == '7' ||
                c == '8' ||
                c == '9');
        }

        private bool IsHex(char c)
        {
            return (char.IsDigit(c) ||
                c == 'A' ||
                c == 'B' ||
                c == 'C' ||
                c == 'D' ||
                c == 'E' ||
                c == 'F' ||
                c == 'a' ||
                c == 'b' ||
                c == 'c' ||
                c == 'd' ||
                c == 'e' ||
                c == 'f');
        }

        private string FromHex(string hex)
        {
            int utf32 = Convert.ToInt32(hex, 16);

            if (utf32 < 0 || utf32 > 0x10FFFF)
                throw new FormatException("\"0x" + utf32.ToString("X8") + "\" is not a valid unicode value.");

            return char.ConvertFromUtf32(utf32);
        }
    }
}
