using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2020.Infrastructure;

namespace AdventOfCode2020.Puzzles.Day06
{
    public class Part1 : Puzzle<IEnumerable<PlaneGroup>, int>
    {
        public override int SampleAnswer => 11;

        public override IEnumerable<PlaneGroup> ParseInput(string rawInput)
            => rawInput
                .Split(Environment.NewLine + Environment.NewLine)
                .Where(line => line.Length > 0)
                .Select(group =>
                    new PlaneGroup(
                        group
                            .Split(Environment.NewLine)
                            .Where(x => x.Length > 0)));

        public override int Solve(IEnumerable<PlaneGroup> input)
            => input.Sum(x => x.CountDistinctAnswers());
    }
}
