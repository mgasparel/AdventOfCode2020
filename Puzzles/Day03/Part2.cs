using System.Collections.Generic;
using System.Linq;
using AdventOfCode2020.Puzzles.Common;

namespace AdventOfCode2020.Puzzles.Day03
{
    public class Part2 : Part1
    {
        public override long SampleAnswer => 336;

        List<(int right, int down)> scenarios = new()
            {
                (1, 1),
                (3, 1),
                (5, 1),
                (7, 1),
                (1, 2)
            };

        public override long Solve(RepeatingMap input)
            => scenarios.Aggregate(1L, (agg, cur) => agg *= SolveWithSlope(input, cur.right, cur.down));

        long SolveWithSlope(RepeatingMap map, int right, int down)
        {
            map.Reset();

            int trees = 0;
            Point lastPosition;
            while(true)
            {
                lastPosition = map.Position;
                map.Move(right, down);

                // If we haven't moved, that means we hit the bottom of the map.
                if (lastPosition == map.Position)
                {
                    return trees;
                }

                if (map.IsAtTree)
                {
                    trees++;
                }
            }
        }
    }
}
