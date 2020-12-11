using AdventOfCode2020.Infrastructure;
using AdventOfCode2020.Puzzles.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Puzzles.Day10
{
    public class Part1 : Puzzle<IEnumerable<int>, long>
    {
        public override long SampleAnswer => 220;

        public override IEnumerable<int> ParseInput(string rawInput)
            => rawInput
                .Split(Environment.NewLine)
                .Where(line => line.Length > 0)
                .Select(line => int.Parse(line));

        public override long Solve(IEnumerable<int> input)
        {
            var deltas = GetDeltas(input.OrderBy(x => x).ToArray());
            return deltas.Count(x => x == 1) * deltas.Count(x => x == 3);
        }

        protected int[] GetDeltas(int[] values)
        {
            var deltas = new int[values.Length + 1];
            for (var i = 1; i < values.Length; i++)
            {
                deltas[i] = values[i] - values[i - 1];
            }

            deltas[0] = values[0];        // joltage between outlet and first adapter.
            deltas[values.Length] = 3;    // joltage between last adapter and device.

            return deltas;
        }
    }
}
