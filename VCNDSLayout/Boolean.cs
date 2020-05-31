using System;

namespace JSON
{
    public class Boolean : Value
    {
        public readonly bool Value;

        public Boolean()
            : base(Type.Boolean)
        {
            Value = false;
        }

        public Boolean(bool value)
            : base(Type.Boolean)
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
            return Value ? "true" : "false";
        }

        public override string ToString(string tab)
        {
            return Value ? "true" : "false";
        }
    }
}