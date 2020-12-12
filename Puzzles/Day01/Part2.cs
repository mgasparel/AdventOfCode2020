using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2020.Infrastructure;

namespace AdventOfCode2020.Puzzles.Day01
{
    public class Part2 : Puzzle<IEnumerable<int>, int>
    {
        const int Target = 2020;

        public override int SampleAnswer => 241861950;

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
                    foreach (int c in input)
                    {
                        if (a + b + c == Target)
                        {
                            return a * b * c;
                        }
                    }
                }
            }

            return 0;
        }
    }
}
