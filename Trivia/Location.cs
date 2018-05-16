namespace Trivia
{
    public class Location
    {
        public static readonly Location Start = new Location(0);
        public static readonly Location Boundary = new Location(11);

        public int Value { get; }

        public Location(int value)
        {
            Value = value;
        }

        public Location Advance(int offset)
        {
            return new Location(Value + offset).AdjustForBoundary();
        }

        private Location AdjustForBoundary()
        {
            return Value > Boundary.Value ? new Location(Value - (Boundary.Value + 1)) : this;
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
            return $"Location ({Value})";
        }
    }
}
