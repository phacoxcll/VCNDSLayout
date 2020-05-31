using System;
using System.IO;
using System.Text;

namespace JSON
{
    public class SyntacticAnalyzer
    {
        private LexicalAnalyzer Lex;
        private Token Current;

        public SyntacticAnalyzer(StreamReader source)
        {
            Lex = new LexicalAnalyzer(source);
        }

        private void Next()
        {
            Current = Lex.GetNextToken();
        }

        private void Match(int label)
        {
            if (Current.Label != label)
                throw new Exception(label.ToString() + " does not match " + Current.Label.ToString() + ".");
            Next();
        }

        public Element Run()
        {
            Next();
            Element element = _Element();

            if (Current.Label != 0)
                throw new Exception("There is still data to read.");

            return element;
        }

        private Element _Element()
        {
            Element element;

            element = new Element(_Value());

            return element;
        }

        private Elements _Elements()
        {
            Elements _elements;

            Element element = _Element();
            Elements elements = null;

            if (Current.Label == WordLabel.Comma)
            {
                Match(WordLabel.Comma);
                elements = _Elements();
            }

            _elements = new Elements(element, elements);

            return _elements;
        }

        private Member _Member()
        {
            Member member;

            if (Current.Label != WordLabel.String)
                throw new Exception("The member's name is not a string.");

            JSON.String jsonStr = new JSON.String(((StringToken)Current).Value);
            Next();
            Match(WordLabel.Colon);
            Element element = new Element(_Value());

            member = new Member(jsonStr, element);

            return member;
        }

        private Members _Members()
        {
            Members _members;

            Member member = _Member();
            Members members = null;

            if (Current.Label == WordLabel.Comma)
            {
                Match(WordLabel.Comma);
                members = _Members();
            }

            _members = new Members(member, members);

            return _members;
        }

        private JSON.Value _Value()
        {
            JSON.Value value;

            switch (Current.Label)
            {
                case WordLabel.String:
                    value = JSON_String();
                    break;
                case WordLabel.Number:
                    value = JSON_Number();
                    break;
                case WordLabel.LeftCurlyBracket:
                    value = JSON_Object();
                    break;
                case WordLabel.LeftSquareBracket:
                    value = JSON_Array();
                    break;
                case WordLabel.True:
                    value = new JSON.Boolean(true);
                    Next();
                    break;
                case WordLabel.False:
                    value = new JSON.Boolean(false);
                    Next();
                    break;
                case WordLabel.Null:
                    value = JSON.Value.Null;
                    Next();
                    break;
                default:
                    value = JSON.Value.Null;
                    break;
            }

            return value;
        }

        private JSON.Value JSON_String()
        {
            JSON.Value value;
            value = new JSON.String(((StringToken)Current).Value);
            Next();
            return value;
        }

        private JSON.Value JSON_Number()
        {
            JSON.Value value;
            value = new JSON.Number(((NumberToken)Current).Value);
            Next();
            return value;
        }

        private JSON.Value JSON_Object()
        {
            JSON.Value value;
            Match(WordLabel.LeftCurlyBracket);
            if (Current.Label == WordLabel.RightCurlyBracket)
                value = new JSON.Object();
            else
                value = new JSON.Object(_Members());
            Match(WordLabel.RightCurlyBracket);
            return value;
        }

        private JSON.Value JSON_Array()
        {
            JSON.Value value;

            Match(WordLabel.LeftSquareBracket);
            if (Current.Label == WordLabel.RightSquareBracket)
                value = new JSON.Array();
            else
                value = new JSON.Array(_Elements());
            Match(WordLabel.RightSquareBracket);

            return value;
        }

        public static string Format(string source)
        {
            Stream s = new MemoryStream(Encoding.UTF8.GetBytes(source));
            StreamReader sr = new StreamReader(s);
            JSON.SyntacticAnalyzer syn = new JSON.SyntacticAnalyzer(sr);
            JSON.Element json = syn.Run();
            sr.Close();
            s.Close();
            return json.ToString("");
        }
    }
}
