namespace JSON
{
    public class Member : Node
    {
        public JSON.String Name;
        public JSON.Element Element;

        public Member()
        {
            Name = new JSON.String();
            Element = new JSON.Element();
        }

        public Member(Member member)
        {
            Name = member.Name;
            Element = member.Element;
        }

        public Member(string name, Value value)
            : this(new JSON.String(name), new Element(value))
        {
        }

        public Member(string name, Element element)
            : this(new JSON.String(name), element)
        {
        }

        public Member(JSON.String name, Value value)
            : this(name, new Element(value))
        {
        }

        public Member(JSON.String name, Element element)
        {
            Name = name;
            Element = element;
        }

        public override string ToString()
        {
            return Name.ToString() + ": " + Element.ToString();
        }

        public override string ToString(string tab)
        {
            return Name.ToString() + ": " + Element.ToString();
        }
    }
}