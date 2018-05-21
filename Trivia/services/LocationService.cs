using System;
using trivia.models;

namespace trivia.services
{
    public interface ILocationService
    {
        Location AdvanceBy(Location current, int offset);
    }

    public class LocationService : ILocationService
    {
        private const int BoundaryPoint = 12;

        public Location AdvanceBy(Location current, int offset)
        {
            if (offset <= 0)
                throw new ArgumentException();

            return new Location((current.Value + offset) % BoundaryPoint);
        }
    }
}
