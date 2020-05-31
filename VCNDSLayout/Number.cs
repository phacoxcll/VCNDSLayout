using System;

namespace JSON
{
    public class Number : Value
    {
        public double Value { get; private set; }

        public Number()
            : base(Type.Number)
        {
            Value = 0;
        }

        public Number(double value)
            : base(Type.Number)
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
            return Value.ToString();
        }

        public override string ToString(string tab)
        {
            return Value.ToString();
        }
    }
}
