namespace JSON
{
    public abstract class Value : Node
    {
        public Type Type;

        public Value(Type type)
        {
            Type = type;
        }

        public abstract Value GetValue(int index);

        public abstract Value GetValue(string name);

        public abstract void SetValue(int index, Value value);

        public abstract void SetValue(string name, Value value);

        public readonly static JSON.Value Null = new Null();
    }
}
