namespace JSON
{
    public class Token
    {
        public readonly int Label;

        public Token(int label)
        {
            Label = label;
        }

        public override string ToString()
        {
            return Label.ToString();
        }
    }
}
