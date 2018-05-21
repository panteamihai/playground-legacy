using trivia.models;

namespace trivia.tests.models
{
    public class ValueObjectFake : ValueObject
    {
        public ValueObjectFake(int value) : base(value) { }

        public override bool Equals(object obj)
        {
            if (obj is ValueObjectFake vof)
                return Value == vof.Value;

            return false;
        }
    }
}