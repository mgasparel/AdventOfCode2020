using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2020.Infrastructure;

namespace AdventOfCode2020.Puzzles.Day01
{
    public class Part1 : Puzzle<IEnumerable<int>, int>
    {
        const int Target = 2020;

        public override int SampleAnswer => 514579;

        public override IEnumerable<int> ParseInput(string rawInput)
            => rawInput
                .Split(Environment.NewLine)
                .Where(line => line.Length > 0)
                .Select(int.Parse);

        public override int Solve(IEnumerable<int> input)
        {
            foreach (int a in input)
            {
                foreach (int b in input)
                {
                    if (a + b == Target)
                    {
                        return a * b;
                    }
                }
            }

            return 0;
        }
    }
}
