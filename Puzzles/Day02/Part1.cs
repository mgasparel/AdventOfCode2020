using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2020.Infrastructure;

namespace AdventOfCode2020.Puzzles.Day02
{
    public record Password(string Value);
    public record Policy(int Min, int Max, char Character);

    public class Part1 : Puzzle<IEnumerable<(Policy pol, Password pass)>, int>
    {
        public override int SampleAnswer => 2;

        public override IEnumerable<(Policy pol, Password pass)> ParseInput(string rawInput)
            => rawInput
                .Split(Environment.NewLine)
                .Where(line => line.Length > 0)
                .Select(ParseTuple);

        (Policy pol, Password pass) ParseTuple(string line)
        {
            int hyphen = line.IndexOf('-');
            int space1 = line.IndexOf(' ');
            int space2 = line.LastIndexOf(' ');

            int min = int.Parse(line[..hyphen]);
            int max = int.Parse(line[(hyphen + 1)..space1]);
            char c = line[space1 + 1];

            return (new Policy(min, max, c), new Password(line[space2..]));
        }

        public override int Solve(IEnumerable<(Policy pol, Password pass)> input)
            => input
                .Where(tup => IsPassValid(tup.pass, tup.pol))
                .Count();

        bool IsPassValid(Password password, Policy policy)
        {
            int count = password.Value
                .Where(c => c == policy.Character)
                .Count();

            return count >= policy.Min && count <= policy.Max;
        }
    }
}
