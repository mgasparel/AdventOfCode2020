using AdventOfCode2020.Infrastructure;
using System;
using System.Linq;

namespace AdventOfCode2020.Puzzles.Day09
{
    public class Part1 : Puzzle<long[], long>
    {
        public override long SampleAnswer => 127;

        protected int preamble = 25;

        public override long[] ParseInput(string rawInput)
            => rawInput
                .Split(Environment.NewLine)
                .Where(line => line.Length > 0)
                .Select(line => long.Parse(line))
                .ToArray();

        public override long Solve(long[] input)
            => FindInvalidNumber(input) ?? throw new System.Exception("Result not found!");

        public override bool ValidateSample(long[] input)
        {
            // First time we hit a sample with different params as the solution.
            preamble = 5;

            return base.ValidateSample(input);
        }

        protected long? FindInvalidNumber(long[] input)
        {
            for(int i = preamble; i < input.Length; i++)
            {
                var window = input[(i - preamble)..(i)];

                if (TargetSumExistsInWindow(input[i], window) == false)
                {
                    return input[i];
                }
            }

            return null;    // not found
        }

        private bool TargetSumExistsInWindow(long target, long[] window)
        {
            bool found = false;
            for (var j = 0; j < window.Length; j++)
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
