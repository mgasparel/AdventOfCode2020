using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Puzzles.Day02
{
    public class Part2 : Part1
    {
        public override int SampleAnswer => 1;

        public override int Solve(IEnumerable<(Policy pol, Password pass)> input)
            => input
                .Where(tup => IsPassValid(tup.pass, tup.pol))
                .Count();

        bool IsPassValid(Password password, Policy policy)
            => (password.Value[policy.Min] == policy.Character)
                ^ (password.Value[policy.Max] == policy.Character);
    }
}
