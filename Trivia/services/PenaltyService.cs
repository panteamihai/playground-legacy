using System;
using trivia.models;

namespace trivia.services
{
    public interface IPenaltyService
    {
        void Incur(Player player);

        void TemporarilyOvercome(Player player);

        void ResetHope(Player player);

        bool CanTemporarilyOvercomePenalty(int roll);

        bool HasIncurredPenalty(Player player);

        bool HasTemporarilyOvercomePenalty(Player player);
    }

    public class PenaltyService : IPenaltyService
    {
        public void Incur(Player player)
        {
            player.TransitionPenaltyTo(Penalty.Incurred);
        }

        public void TemporarilyOvercome(Player player)
        {
            if (player.Penalty != Penalty.Incurred)
                throw new InvalidOperationException();

            player.TransitionPenaltyTo(Penalty.TemporarilyOvercame);
        }

        public void ResetHope(Player player)
        {
            if (player.Penalty == Penalty.TemporarilyOvercame)
                player.TransitionPenaltyTo(Penalty.Incurred);
        }

        public bool CanTemporarilyOvercomePenalty(int roll)
        {
            return roll % 2 != 0;
        }

        public bool HasIncurredPenalty(Player player)
        {
            return player.Penalty != Penalty.None;
        }

        public bool HasTemporarilyOvercomePenalty(Player player)
        {
            return player.Penalty == Penalty.TemporarilyOvercame;
        }
    }
}
