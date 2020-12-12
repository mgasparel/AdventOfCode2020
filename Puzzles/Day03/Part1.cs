using System;
using System.Linq;
using AdventOfCode2020.Infrastructure;
using AdventOfCode2020.Puzzles.Common;

namespace AdventOfCode2020.Puzzles.Day03
{
    public class Part1 : Puzzle<RepeatingMap, long>
    {
        public override long SampleAnswer => 7;

        public override RepeatingMap ParseInput(string rawInput)
            => new RepeatingMap(
                rawInput
                    .Split(Environment.NewLine)
                    .Where(line => line.Length > 0)
                    .ToArray());

        public override long Solve(RepeatingMap input)
        {
            int trees = 0;
            Point lastPosition;
            while (true)
            {
                lastPosition = input.Position;
                input.Move(3, 1);

                // If we haven't moved, that means we hit the bottom of the map.
                if (lastPosition == input.Position)
                {
                    return trees;
                }

                if (input.IsAtTree)
                {
                    trees++;
                }
            }
        }
    }
}
