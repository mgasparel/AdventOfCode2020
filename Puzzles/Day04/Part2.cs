using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Puzzles.Day04
{
    public class Part2 : Part1
    {
        public override int SampleAnswer => 2;

        public override IEnumerable<Passport> ParseInput(string rawInput)
        {
            return rawInput
                .Split(Environment.NewLine + Environment.NewLine)
                .Where(x => x.Length > 0)
                .Select(ParsePassport);
        }

        public override int Solve(IEnumerable<Passport> input)
            => input.Count(p => p.IsStrictValid());
    }
}
