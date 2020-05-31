using System;

namespace JSON
{
    public class String : Value
    {
        public string Value { get; private set; }

        public String()
            : base(Type.String)
        {
            Value = "";
        }

        public String(string value)
            : base(Type.String)
        {
            Value = value;
        }

        public override Value GetValue(int index)
        {
            throw new NotImplementedException("This type of value does not have multiple elements.");
        }

        public override Value GetValue(string name)
        {
            throw new NotImplementedException("This type of value does not have multiple elements.");
        }

        public override void SetValue(int index, Value value)
        {
            throw new NotImplementedException("This type of value does not have multiple elements.");
        }

        public override void SetValue(string name, Value value)
        {
            throw new NotImplementedException("This type of value does not have multiple elements.");
        }

        public override string ToString()
        {
            if (Value != null)
                return "\"" + Value + "\"";
            else
                return "null";
        }

        public override string ToString(string tab)
        {
            if (Value != null)
            {
                Value = Value.Replace("\\", "\\\\");
                Value = Value.Replace("\"", "\\\"");
                Value = Value.Replace("\b", "\\b");
                Value = Value.Replace("\f", "\\f");
                Value = Value.Replace("\n", "\\n");
                Value = Value.Replace("\r", "\\r");
                Value = Value.Replace("\t", "\\t");
                return "\"" + Value + "\"";
            }
            else
                return "null";
        }
    }
}
