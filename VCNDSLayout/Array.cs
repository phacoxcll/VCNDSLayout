using System;
using System.Collections.Generic;

namespace JSON
{
    public class Array : Value
    {
        private JSON.Elements Elements;

        public Array()
            : base(Type.Array)
        {
            Elements = null;
        }

        public Array(Elements elements)
            : base(Type.Array)
        {
            Elements = elements;
        }

        public Array(object[] values)
            : base(Type.Array)
        {
            foreach (object value in values)
                AddValue(value);
        }

        public int Count
        {
            get
            {
                if (Elements == null)
                    return 0;
                else
                    return Elements.Count;
            }
        }

        public override Value GetValue(int index)
        {
            int i = 0;
            JSON.Elements elements = Elements;

            while (elements != null)
            {
                if (i == index)
                    return elements._Element.Value;
                else
                {
                    i++;
                    elements = elements._Elements;
                }
            }

            throw new System.IndexOutOfRangeException("The index is outside the range of the array length.");
        }

        public override Value GetValue(string name)
        {
            throw new NotImplementedException("The elements of an array do not have names.");
        }

        public override void SetValue(int index, Value value)
        {
            int i = 0;
            JSON.Elements elements = Elements;

            while (elements != null)
            {
                if (i == index)
                {
                    elements._Element = new JSON.Element(value);
                    break;
                }
                else
                {
                    i++;
                    elements = elements._Elements;
                }
            }

            if (elements == null)
                throw new System.IndexOutOfRangeException("The index is outside the range of the array length.");
        }

        public override void SetValue(string name, Value value)
        {
            throw new NotImplementedException("The elements of an array do not have names.");
        }

        public void DeleteValue(int index)
        {
            if (index == 0)
            {
                if (Elements != null)
                    Elements = Elements._Elements;
            }
            else
            {
                int i = 0;
                JSON.Elements elements = Elements;

                while (elements != null)
                {
                    if (i == index - 1)
                    {
                        if (elements._Elements != null)
                            elements._Elements = elements._Elements._Elements;
                        break;
                    }
                    else
                    {
                        i++;
                        elements = elements._Elements;
                    }
                }
            }
        }
        
        public void AddValue(object value)
        {
            if (value is string)
                AddValue(new JSON.String((string)value));
            else if (value is decimal)
                AddValue(new JSON.Number((double)(decimal)value));
            else if (value is double)
                AddValue(new JSON.Number((double)value));
            else if (value is float)
                AddValue(new JSON.Number((float)value));
            else if (value is ulong)
                AddValue(new JSON.Number((ulong)value));
            else if (value is long)
                AddValue(new JSON.Number((long)value));
            else if (value is uint)
                AddValue(new JSON.Number((uint)value));
            else if (value is int)
                AddValue(new JSON.Number((int)value));
            else if (value is ushort)
                AddValue(new JSON.Number((ushort)value));
            else if (value is short)
                AddValue(new JSON.Number((short)value));
            else if (value is byte)
                AddValue(new JSON.Number((byte)value));
            else if (value is sbyte)
                AddValue(new JSON.Number((sbyte)value));
            else if (value is bool)
                AddValue(new JSON.Boolean((bool)value));
            else if (value is null)
                AddValue(new JSON.Null());
            else if (value is object[])
                AddValue(new JSON.Array((object[])value));
            else if (value is KeyValuePair<string, object>[])
                AddValue(new JSON.Object((KeyValuePair<string, object>[])value));
            else
                AddValue(new JSON.String(value.ToString()));
        }

        public void AddValue(Value value)
        {
            if (Elements == null)
            {
                Elements = new Elements(new Element(value), null);
            }
            else
            {
                JSON.Elements elements = Elements;

                while (elements._Elements != null)
                {
                    elements = elements._Elements;
                }

                elements._Elements = new Elements(new Element(value), null);
            }
        }

        public void InsertValue(int index, Value value)
        {
            if (index == 0)
            {
                Elements = new Elements(new Element(value), Elements);
            }
            else
            {
                int i = 0;
                JSON.Elements elements = Elements;

                while (elements != null)
                {
                    if (i == index - 1)
                    {
                        elements._Elements = new Elements(new Element(value), elements._Elements);
                        break;
                    }
                    else
                    {
                        i++;
                        elements = elements._Elements;
                    }
                }

                if (elements == null)
                    throw new System.IndexOutOfRangeException("The index is outside the range of the array length.");
            }
        }

        public override string ToString()
        {
            if (Elements == null)
                return "[]";
            else
                return "[" + Elements.ToString() + "]";
        }

        public override string ToString(string tab)
        {
            if (Elements == null)
                return tab + "[]";
            else
                return "[\n" + Elements.ToString(tab + "\t") + "\n" + tab + "]";
        }
    }
}
