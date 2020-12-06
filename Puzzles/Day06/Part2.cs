using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Puzzles.Day06
{
    public class Part2 : Part1
    {
        public override int SampleAnswer => 6;

        public override int Solve(IEnumerable<PlaneGroup> input)
            => input.Sum(x => x.CountIntersectingAnswers());
    }
}
