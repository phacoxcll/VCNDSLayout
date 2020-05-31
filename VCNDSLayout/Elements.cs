using System.Text;

namespace JSON
{
    public class Elements : Node
    {
        public JSON.Element _Element;
        public JSON.Elements _Elements;

        public Elements(Element element, Elements elements)
        {
            _Element = element;
            _Elements = elements;
        }

        public int Count
        {
            get
            {
                if (_Element == null)
                    return 0;

                int i = 1;
                Elements elements = _Elements;
                while (elements != null)
                {
                    i++;
                    elements = elements._Elements;
                }
                return i;
            }
        }

        public override string ToString()
        {
            StringBuilder strBuilder = new StringBuilder();

            strBuilder.Append(_Element.Value.ToString());

            Elements elements = _Elements;
            while (elements != null)
            {
                strBuilder.Append(", " + elements._Element.Value.ToString());
                elements = elements._Elements;
            }

            return strBuilder.ToString();
        }

        public override string ToString(string tab)
        {
            StringBuilder strBuilder = new StringBuilder();

            strBuilder.Append(tab + _Element.Value.ToString(tab));

            Elements elements = _Elements;
            while (elements != null)
            {
                strBuilder.Append(",\n" + tab + elements._Element.Value.ToString(tab));
                elements = elements._Elements;
            }

            return strBuilder.ToString();
        }
    }
}
