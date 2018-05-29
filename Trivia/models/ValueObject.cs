using System;

namespace trivia.models
{
    public abstract class ValueObject
    {
        public int Value { get; }

        protected ValueObject(int value)
        {
            if (value < 0)
                throw new ArgumentException();

            Value = value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public abstract override bool Equals(object obj);

        public override string ToString()
        {
            return Value.ToString();
        }

        public static implicit operator int(ValueObject vo)
        {
            return vo.Value;
        }
    }
}
