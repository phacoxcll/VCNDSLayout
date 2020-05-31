namespace JSON
{
    public class Word : Token
    {
        public readonly string Lexeme;

        public Word(string lexeme, int label)
            : base(label)
        {
            Lexeme = lexeme;
        }

        public readonly static Word
            False = new Word(WordString.False, WordLabel.False),
            True = new Word(WordString.True, WordLabel.True);
    }
}