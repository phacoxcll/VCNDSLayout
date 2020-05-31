namespace JSON
{
    public class StringToken : Token
    {
        public readonly string Value;

        public StringToken(string value, int label)
            : base(label)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
