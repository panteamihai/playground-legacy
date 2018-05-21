namespace trivia.models
{
    public class CoinBalance : ValueObject
    {
        public static readonly CoinBalance Empty = new CoinBalance(0);

        public CoinBalance(int value) : base(value) { }

        public override bool Equals(object obj)
        {
            if (obj is CoinBalance balance)
                return Value == balance.Value;

            return false;
        }
    }
}
