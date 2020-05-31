namespace JSON
{
    public class Type : Word
    {
        public Type(string lexeme, int label)
            : base(lexeme, label)
        {
        }

        public readonly static Type
            Null = new Type(WordString.Null, WordLabel.Null),
            Boolean = new Type(WordString.Boolean, WordLabel.Value),
            String = new Type(WordString.String, WordLabel.Value),
            Number = new Type(WordString.Number, WordLabel.Value),
            Array = new Type(WordString.Array, WordLabel.Value),
            Object = new Type(WordString.Object, WordLabel.Value);
    }
}