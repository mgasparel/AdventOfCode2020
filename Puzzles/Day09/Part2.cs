using System.Linq;

namespace AdventOfCode2020.Puzzles.Day09
{
    public class Part2 : Part1
    {
        public override long SampleAnswer => 62;

        public override long Solve(long[] input)
        {
            long invalidNumber = FindInvalidNumber(input) ?? throw new System.Exception("Result not found!");
            (long Start, long End) range = FindRangeSummingTo(invalidNumber, input)
                ?? throw new System.Exception("Result not found!");

            return range!.Start + range!.End;
        }

        static (long Start, long End)? FindRangeSummingTo(long target, long[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                long sum = 0;
                for (int j = i; j < input.Length; j++)
                {
                    if ((sum += input[j]) == target)
                    {
                        return (input[i..j].Min(), input[i..j].Max());
                    }

                    if (sum > target)
                    {
                        break;
                    };
                }
            }

            return null;
        }
    }
}
