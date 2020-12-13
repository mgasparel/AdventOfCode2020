using AdventOfCode2020.Infrastructure;
using AdventOfCode2020.Puzzles.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Puzzles.Day12
{
    public class Part1 : Puzzle<IEnumerable<NavInstruction>, int>
    {
        public override int SampleAnswer => 25;

        public override IEnumerable<NavInstruction> ParseInput(string rawInput)
            => rawInput
                .Split(Environment.NewLine)
                .Where(line => line.Length > 0)
                .Select(line =>
                    new NavInstruction(
                        Action: line[0],
                        Value: int.Parse(line[1..])
                    ));

        public override int Solve(IEnumerable<NavInstruction> input)
        {
            var origin = new Point(0, 0);
            var ferry = new Ferry(origin);
            ferry.Sail(input);
            return ManhattanDistance(origin, ferry.Location);
        }

        protected static int ManhattanDistance(Point a, Point b)
            => Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
    }
}
