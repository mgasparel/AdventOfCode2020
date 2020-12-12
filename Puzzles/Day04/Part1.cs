using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2020.Infrastructure;

namespace AdventOfCode2020.Puzzles.Day04
{
    public class Part1 : Puzzle<IEnumerable<Passport>, int>
    {
        public override int SampleAnswer => 2;

        public override IEnumerable<Passport> ParseInput(string rawInput)
        {
            return rawInput
                .Split(Environment.NewLine + Environment.NewLine)
                .Where(x => x.Length > 0)
                .Select(ParsePassport);
        }

        protected static Passport ParsePassport(string chunk)
        {
            IEnumerable<KeyValuePair<string, string>>? keyValuePairs = chunk
                .Replace(Environment.NewLine, " ")
                .TrimEnd()
                .Split(' ')
                .Select(pair => new KeyValuePair<string, string>(pair[..3], pair[4..]));

            return new Passport(new Dictionary<string, string>(keyValuePairs));
        }

        public override int Solve(IEnumerable<Passport> input)
            => input.Count(p => p.IsValid());
    }
}
