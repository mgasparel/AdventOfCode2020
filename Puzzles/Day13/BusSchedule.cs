using System.Collections.Generic;

namespace AdventOfCode2020.Puzzles.Day13
{
    public class BusSchedule
    {
        public int Now { get; }

        public IEnumerable<int> BusIds { get; set; }

        public BusSchedule(int now, IEnumerable<int> busIds)
        {
            Now = now;
            BusIds = busIds;
        }
    }
}
