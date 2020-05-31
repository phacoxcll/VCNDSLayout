using System.Text;

namespace JSON
{
    public class Members : Node
    {
        public JSON.Member _Member;
        public JSON.Members _Members;

        public Members(Member member, Members members)
        {
            _Member = member;
            _Members = members;
        }

        public int Count
        {
            get
            {
                if (_Member == null)
                    return 0;

                int i = 1;
                Members members = _Members;
                while (members != null)
                {
                    i++;
                    members = members._Members;
                }
                return i;
            }
        }

        public Member GetMember(int index)
        {
            int i = 0;
            Members members = this;

            while (members != null)
            {
                if (i == index)
                    return members._Member;
                else
                {
                    i++;
                    members = members._Members;
                }
            }

            return new Member();
        }

        public void SetMember(Member member, int index)
        {
            int i = 0;
            Members members = this;

            while (members != null)
            {
                if (i == index)
                {
                    members._Member = member;
                    break;
                }
                else
                {
                    i++;
                    members = members._Members;
                }
            }
        }

        public override string ToString()
        {
            StringBuilder strBuilder = new StringBuilder();

            strBuilder.Append(_Member.Name.ToString() + ": " + _Member.Element.ToString());

            Members members = _Members;
            while (members != null)
            {
                strBuilder.Append(", " + members._Member.Name.ToString() + ": " + members._Member.Element.ToString());
                members = members._Members;
            }

            return strBuilder.ToString();
        }

        public override string ToString(string tab)
        {
            StringBuilder strBuilder = new StringBuilder();

            strBuilder.Append(tab + _Member.Name.ToString() + ": " + _Member.Element.ToString(tab));

            Members members = _Members;
            while (members != null)
            {
                strBuilder.Append(",\n" + tab + members._Member.Name.ToString() + ": " + members._Member.Element.ToString(tab));
                members = members._Members;
            }

            return strBuilder.ToString();
        }
    }
}
