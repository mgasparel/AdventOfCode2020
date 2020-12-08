using System.Collections.Generic;
using System.Linq;
using AdventOfCode2020.Puzzles.Common;

namespace AdventOfCode2020.Puzzles.Day07
{
    public class Part2 : Part1
    {
        public override long SampleAnswer => 32;

        public override long Solve(Dictionary<string, Node<Bag>> input)
            => CountInnerBagsRecursive(input, input[Target]) - 1;   // We counted the target bag, reduce count by 1.

        long CountInnerBagsRecursive(Dictionary<string, Node<Bag>> allNodes, Node<Bag> node)
            => node.Children.Aggregate(1L, (acc, cur) =>
                acc += cur.Value.Count * CountInnerBagsRecursive(allNodes, allNodes[cur.Name]));
    }
}
