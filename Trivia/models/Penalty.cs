namespace trivia.models
{
    public class Penalty
    {
        public static readonly Penalty None = new Penalty();
        public static readonly Penalty Incurred = new Penalty { HasBeenIncurred = true };
        public static readonly Penalty TemporarilyOvercame = new Penalty { HasBeenIncurred = true, HasBeenTemporarilyOvercame = true };

        public bool HasBeenIncurred { get; private set; }

        public bool HasBeenTemporarilyOvercame { get; private set; }

        public Penalty()
        {
            HasBeenIncurred = false;
            HasBeenTemporarilyOvercame = false;
        }
    }
}
