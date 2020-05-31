using System.Collections.Generic;

namespace JSON
{
    public class Object : Value
    {
        private JSON.Members Members;

        public Object()
            : base(Type.Object)
        {
            Members = null;
        }

        public Object(JSON.Members members)
            : base(Type.Object)
        {
            Members = members;
        }

        public Object(KeyValuePair<string, object>[] values)
            : base(Type.Object)
        {
            foreach (KeyValuePair<string, object> value in values)
                AddMember(value.Key, value.Value);
        }

        public int Count
        {
            get
            {
                if (Members == null)
                    return 0;
                else
                    return Members.Count;
            }
        }

        public bool Contains(string name)
        {
            JSON.Members members = Members;

            while (members != null)
            {
                if (members._Member.Name.Value == name)
                    return true;
                else
                    members = members._Members;
            }

            return false;
        }

        public String GetName(int index)
        {
            int i = 0;
            JSON.Members members = Members;

            while (members != null)
            {
                if (i == index)
                    return members._Member.Name;
                else
                {
                    i++;
                    members = members._Members;
                }
            }

            throw new System.IndexOutOfRangeException("The index is outside the range of the object length.");
        }

        public override Value GetValue(int index)
        {
            int i = 0;
            JSON.Members members = Members;

            while (members != null)
            {
                if (i == index)
                    return members._Member.Element.Value;
                else
                {
                    i++;
                    members = members._Members;
                }
            }

            throw new System.IndexOutOfRangeException("The index is outside the range of the object length.");
        }

        public override Value GetValue(string name)
        {
            return GetFirstValue(name);
        }

        public Value GetFirstValue(string name)
        {
            JSON.Members members = Members;

            while (members != null)
            {
                if (members._Member.Name.Value == name)
                    return members._Member.Element.Value;
                else
                    members = members._Members;
            }

            throw new System.IndexOutOfRangeException("Does not contain the member \"" + name + "\".");
        }

        public void SetName(int index, string name)
        {
            int i = 0;
            JSON.Members members = Members;

            while (members != null)
            {
                if (i == index)
                {
                    members._Member.Name = new JSON.String(name);
                    break;
                }
                else
                {
                    i++;
                    members = members._Members;
                }
            }

            if (members == null)
                throw new System.IndexOutOfRangeException("The index is outside the range of the object length.");
        }

        public override void SetValue(int index, Value value)
        {
            int i = 0;
            JSON.Members members = Members;

            while (members != null)
            {
                if (i == index)
                {
                    members._Member.Element = new JSON.Element(value);
                    break;
                }
                else
                {
                    i++;
                    members = members._Members;
                }
            }

            if (members == null)
                throw new System.IndexOutOfRangeException("The index is outside the range of the object length.");
        }

        public override void SetValue(string name, Value value)
        {
            SetFirstValue(name, value);
        }

        public void SetFirstValue(string name, Value value)
        {
            JSON.Members members = Members;

            while (members != null)
            {
                if (members._Member.Name.Value == name)
                {
                    members._Member.Element = new JSON.Element(value);
                    break;
                }
                else
                    members = members._Members;
            }
        }


        public void DeleteValue(int index)
        {
            if (index == 0)
            {
                if (Members != null)
                    Members = Members._Members;
            }
            else
            {
                int i = 0;
                JSON.Members members = Members;

                while (members != null)
                {
                    if (i == index - 1)
                    {
                        if (members._Members != null)
                            members._Members = members._Members._Members;
                        break;
                    }
                    else
                    {
                        i++;
                        members = members._Members;
                    }
                }

                if (members == null)
                    throw new System.IndexOutOfRangeException("The index is outside the range of the object length.");
            }
        }

        public void DeleteFirtsValue(string name)
        {
            if (Members != null && Members._Member.Name.Value == name)
            {
                Members = Members._Members;
            }
            else
            {
                JSON.Members members = Members;

                while (members != null)
                {
                    if (members._Members != null && members._Members._Member.Name.Value == name)
                    {
                        members._Members = members._Members._Members;
                        break;
                    }
                    else
                        members = members._Members;
                }

                if (members == null)
                    throw new System.IndexOutOfRangeException("Does not contain the member \"" + name + "\".");
            }
        }


        public void AddMember(string name)
        {
            AddMember(new Member(new JSON.String(name), new Element()));
        }

        public void AddMember(string name, object value)
        {
            if (value is string)
                AddMember(new Member(name, new JSON.String((string)value)));
            else if (value is decimal)
                AddMember(new Member(name, new JSON.Number((double)(decimal)value)));
            else if (value is double)
                AddMember(new Member(name, new JSON.Number((double)value)));
            else if (value is float)
                AddMember(new Member(name, new JSON.Number((float)value)));
            else if (value is ulong)
                AddMember(new Member(name, new JSON.Number((ulong)value)));
            else if (value is long)
                AddMember(new Member(name, new JSON.Number((long)value)));
            else if (value is uint)
                AddMember(new Member(name, new JSON.Number((uint)value)));
            else if (value is int)
                AddMember(new Member(name, new JSON.Number((int)value)));
            else if (value is ushort)
                AddMember(new Member(name, new JSON.Number((ushort)value)));
            else if (value is short)
                AddMember(new Member(name, new JSON.Number((short)value)));
            else if (value is byte)
                AddMember(new Member(name, new JSON.Number((byte)value)));
            else if (value is sbyte)
                AddMember(new Member(name, new JSON.Number((sbyte)value)));
            else if (value is bool)
                AddMember(new Member(name, new JSON.Boolean((bool)value)));
            else if (value is null)
                AddMember(name);
            else if (value is object[])
                AddMember(new Member(name, new JSON.Array((object[])value)));
            else if (value is KeyValuePair<string, object>[])
                AddMember(new Member(name, new JSON.Object((KeyValuePair<string, object>[])value)));
            else
                AddMember(new Member(name, new JSON.String(value.ToString())));
        }

        public void AddMember(string name, Value value)
        {
            AddMember(new Member(name, value));
        }

        public void AddMember(KeyValuePair<string, object> value)
        {
            AddMember(value.Key, value.Value);
        }

        private void AddMember(Member member)
        {
            if (Members == null)
            {
                Members = new Members(member, null);
            }
            else
            {
                JSON.Members members = Members;

                while (members._Members != null)
                {
                    members = members._Members;
                }

                members._Members = new Members(member, null);
            }
        }

        public void InsertMember(int index, string name)
        {
            InsertMember(index, new Member(new JSON.String(name), new Element()));
        }
        
        public void InsertMember(int index, string name, object value)
        {
            if (value is string)
                InsertMember(index, new Member(name, new JSON.String((string)value)));
            else if (value is decimal)
                InsertMember(index, new Member(name, new JSON.Number((double)(decimal)value)));
            else if (value is double)
                InsertMember(index, new Member(name, new JSON.Number((double)value)));
            else if (value is float)
                InsertMember(index, new Member(name, new JSON.Number((float)value)));
            else if (value is ulong)
                InsertMember(index, new Member(name, new JSON.Number((ulong)value)));
            else if (value is long)
                InsertMember(index, new Member(name, new JSON.Number((long)value)));
            else if (value is uint)
                InsertMember(index, new Member(name, new JSON.Number((uint)value)));
            else if (value is int)
                InsertMember(index, new Member(name, new JSON.Number((int)value)));
            else if (value is ushort)
                InsertMember(index, new Member(name, new JSON.Number((ushort)value)));
            else if (value is short)
                InsertMember(index, new Member(name, new JSON.Number((short)value)));
            else if (value is byte)
                InsertMember(index, new Member(name, new JSON.Number((byte)value)));
            else if (value is sbyte)
                InsertMember(index, new Member(name, new JSON.Number((sbyte)value)));
            else if (value is bool)
                InsertMember(index, new Member(name, new JSON.Boolean((bool)value)));
            else if (value is null)
                InsertMember(index, name);
            else if (value is object[])
                InsertMember(index, new Member(name, new JSON.Array((object[])value)));
            else if (value is KeyValuePair<string, object>[])
                InsertMember(index, new Member(name, new JSON.Object((KeyValuePair<string, object>[])value)));
            else
                InsertMember(index, new Member(name, new JSON.String(value.ToString())));
        }

        public void InsertMember(int index, string name, Value value)
        {
            InsertMember(index, new Member(name, value));
        }

        public void InsertMember(int index, KeyValuePair<string, object> value)
        {
            InsertMember(index, value.Key, value.Value);
        }

        private void InsertMember(int index, Member member)
        {
            if (index == 0)
            {
                Members = new Members(member, Members);
            }
            else
            {
                int i = 0;
                JSON.Members members = Members;

                while (members != null)
                {
                    if (i == index - 1)
                    {
                        members._Members = new Members(member, members._Members);
                        break;
                    }
                    else
                    {
                        i++;
                        members = members._Members;
                    }
                }

                if (members == null)
                    throw new System.IndexOutOfRangeException("The index is outside the range of the object length.");
            }
        }

        public void InsertFirtsMember(string nameMatch, string name)
        {
            InsertFirtsMember(nameMatch, new Member(new JSON.String(name), new Element()));
        }

        public void InsertFirtsMember(string nameMatch, string name, object value)
        {
            if (value is string)
                InsertFirtsMember(nameMatch, new Member(name, new JSON.String((string)value)));
            else if (value is decimal)
                InsertFirtsMember(nameMatch, new Member(name, new JSON.Number((double)(decimal)value)));
            else if (value is double)
                InsertFirtsMember(nameMatch, new Member(name, new JSON.Number((double)value)));
            else if (value is float)
                InsertFirtsMember(nameMatch, new Member(name, new JSON.Number((float)value)));
            else if (value is ulong)
                InsertFirtsMember(nameMatch, new Member(name, new JSON.Number((ulong)value)));
            else if (value is long)
                InsertFirtsMember(nameMatch, new Member(name, new JSON.Number((long)value)));
            else if (value is uint)
                InsertFirtsMember(nameMatch, new Member(name, new JSON.Number((uint)value)));
            else if (value is int)
                InsertFirtsMember(nameMatch, new Member(name, new JSON.Number((int)value)));
            else if (value is ushort)
                InsertFirtsMember(nameMatch, new Member(name, new JSON.Number((ushort)value)));
            else if (value is short)
                InsertFirtsMember(nameMatch, new Member(name, new JSON.Number((short)value)));
            else if (value is byte)
                InsertFirtsMember(nameMatch, new Member(name, new JSON.Number((byte)value)));
            else if (value is sbyte)
                InsertFirtsMember(nameMatch, new Member(name, new JSON.Number((sbyte)value)));
            else if (value is bool)
                InsertFirtsMember(nameMatch, new Member(name, new JSON.Boolean((bool)value)));
            else if (value is null)
                InsertFirtsMember(nameMatch, name);
            else if (value is object[])
                InsertFirtsMember(nameMatch, new Member(name, new JSON.Array((object[])value)));
            else if (value is KeyValuePair<string, object>[])
                InsertFirtsMember(nameMatch, new Member(name, new JSON.Object((KeyValuePair<string, object>[])value)));
            else
                InsertFirtsMember(nameMatch, new Member(name, new JSON.String(value.ToString())));
        }

        public void InsertFirtsMember(string nameMatch, string name, Value value)
        {
            InsertFirtsMember(nameMatch, new Member(name, value));
        }

        public void InsertFirtsMember(string nameMatch, KeyValuePair<string, object> value)
        {
            InsertFirtsMember(nameMatch, value.Key, value.Value);
        }

        private void InsertFirtsMember(string nameMatch, Member member)
        {
            if (Members != null && Members._Member.Name.Value == nameMatch)
            {
                Members = new Members(member, Members);
            }
            else
            {
                JSON.Members members = Members;

                while (members != null)
                {
                    if (members._Members != null && members._Members._Member.Name.Value == nameMatch)
                    {
                        members._Members = new Members(member, members._Members);
                        break;
                    }
                    else
                        members = members._Members;
                }

                if (members == null)
                    throw new System.IndexOutOfRangeException("Does not contain the member \"" + nameMatch + "\".");
            }
        }


        public override string ToString()
        {
            if (Members == null)
                return "{}";
            else
                return "{" + Members.ToString() + "}";
        }

        public override string ToString(string tab)
        {
            if (Members == null)
                return tab + "{}";
            else
                return "{\n" + Members.ToString(tab + "\t") + "\n" + tab + "}";
        }
    }
}
