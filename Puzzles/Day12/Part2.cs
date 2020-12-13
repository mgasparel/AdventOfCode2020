using AdventOfCode2020.Puzzles.Common;
using System.Collections.Generic;

namespace AdventOfCode2020.Puzzles.Day12
{
    public class Part2 : Part1
    {
        public override int SampleAnswer => 286;

        public override int Solve(IEnumerable<NavInstruction> input)
        {
            var origin = new Point(0, 0);
            var ferry = new WaypointFerry(origin, new Point(10, 1));
            ferry.Sail(input);
            return ManhattanDistance(origin, ferry.Location);
        }
    }
}
