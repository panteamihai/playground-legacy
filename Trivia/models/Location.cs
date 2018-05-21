namespace trivia.models
{
    public class Location : ValueObject
    {
        public static readonly Location Start = new Location(0);

        public Location(int value) : base(value) { }

        public override bool Equals(object obj)
        {
            if (obj is Location location)
                return Value == location.Value;

            return false;
        }
    }
}
