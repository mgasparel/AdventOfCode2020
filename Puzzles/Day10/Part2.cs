using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Puzzles.Day10
{
    public class Part2 : Part1
    {
        public override long SampleAnswer => 19208;

        public override long Solve(IEnumerable<int> input)
        {
            int[] deltas = GetDeltas(input.OrderBy(x => x).ToArray());

            return GetContiguousOnesCounts(deltas)
                .Aggregate(1L, (sum, cur) => sum *= PossibleCombinations(cur));
        }

        private static IEnumerable<int> GetContiguousOnesCounts(IEnumerable<int> deltas)
        {
            int contiguousOnes = 0;
            foreach (int delta in deltas)
            {
                if (delta == 1)
                {
                    contiguousOnes++;
                    continue;
                }

                if (contiguousOnes == 0)
                {
                    continue;
                }

                yield return contiguousOnes;
                contiguousOnes = 0;
            }
        }

        static long PossibleCombinations(int seqLength)
            => seqLength switch {
                1 => 1,
                2 => 2,
                3 => 4,
                4 => 7,
                5 => 13,
                6 => 22,
                _ => 0
            };
    }
}
