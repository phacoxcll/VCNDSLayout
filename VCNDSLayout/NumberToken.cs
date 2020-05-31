namespace JSON
{
    public class NumberToken : Token
    {
        public readonly double Value;

        public NumberToken(double value, int label)
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
