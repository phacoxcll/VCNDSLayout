using System;

namespace JSON
{
    public class Null : Value
    {
        public readonly string Value = "null";

        public Null()
            : base(Type.Null)
        {
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
            return Value;
        }

        public override string ToString(string tab)
        {
            return Value;
        }
    }
}
