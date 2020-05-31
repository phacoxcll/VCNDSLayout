namespace JSON
{
    public class Element : Node
    {
        public JSON.Value Value;

        public Element()
        {
            Value = JSON.Value.Null;
        }

        public Element(Element element)
        {
            Value = element.Value;
        }

        public Element(JSON.Value value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public override string ToString(string tab)
        {
            return Value.ToString(tab);
        }
    }
}