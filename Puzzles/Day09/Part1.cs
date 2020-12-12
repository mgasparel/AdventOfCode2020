using AdventOfCode2020.Infrastructure;
using System;
using System.Linq;

namespace AdventOfCode2020.Puzzles.Day09
{
    public class Part1 : Puzzle<long[], long>
    {
        public override long SampleAnswer => 127;

        protected int Preamble { get; set; } = 25;

        public override long[] ParseInput(string rawInput)
            => rawInput
                .Split(Environment.NewLine)
                .Where(line => line.Length > 0)
                .Select(long.Parse)
                .ToArray();

        public override long Solve(long[] input)
            => FindInvalidNumber(input) ?? throw new System.Exception("Result not found!");

        public override SampleResult ValidateSample(long[] input)
        {
            // First time we hit a sample with different params as the solution.
            Preamble = 5;

            return base.ValidateSample(input);
        }

        protected long? FindInvalidNumber(long[] input)
        {
            for (int i = Preamble; i < input.Length; i++)
            {
                long[]? window = input[(i - Preamble)..(i)];

                if (TargetSumExistsInWindow(input[i], window) == false)
                {
                    return input[i];
                }
            }

            return null;    // not found
        }

        static bool TargetSumExistsInWindow(long target, long[] window)
        {
            bool found = false;
            for (int j = 0; j < window.Length; j++)
            {
                // Bail early. One half of the sum is already greater than the result.
                if (window[j] > target)
                {
                    continue;
                }

                // If already true, don't set back to false.
                found = window.Any(x => x != window[j] && x + window[j] == target) || found;
            }

            return found;
        }
    }
}
