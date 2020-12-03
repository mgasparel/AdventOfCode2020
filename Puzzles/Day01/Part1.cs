using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2020.Infrastructure;

namespace AdventOfCode2020.Puzzles.Day01
{
    public class Part1 : IPuzzle<IEnumerable<int>, int>
    {
        const int target = 2020;

        public int SampleAnswer => 514579;

        public Part1()
        {
        }

        public IEnumerable<int> ParseInput(string rawInput)
            => rawInput
                .Split(Environment.NewLine)
                .Where(line => line.Length > 0)
                .Select(line => int.Parse(line));

        public int Solve(IEnumerable<int> input)
        {
            foreach(var a in input)
            {
                foreach(var b in input)
                {
                    if (a + b == target)
                    {
                        return a * b;
                    }
                }
            }

            return 0;
        }

        public void ValidateSample(IEnumerable<int> input)
        {
            int result = Solve(input);

            System.Diagnostics.Debug.Assert(result ==  SampleAnswer);
        }
    }
}
