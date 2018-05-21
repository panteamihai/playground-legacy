using System;

namespace Trivia
{
    public class Location
    {
        public static readonly Location Start = new Location(0);

        public int Value { get; }

        public Location(int value)
        {
            if(value < 0)
                throw new ArgumentException();

            Value = value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is Location location)
                return Value == location.Value;

            return false;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
