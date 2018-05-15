namespace Trivia
{
    public class Location
    {
        public static readonly Location Start = new Location(0);

        public int Value { get; }

        public Location(int value)
        {
            Value = value;
        }

        public Location Advance(int offset)
        {
            return new Location(Value + offset);
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
    }
}
