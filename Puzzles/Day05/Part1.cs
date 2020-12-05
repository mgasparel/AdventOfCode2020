using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2020.Infrastructure;

namespace AdventOfCode2020.Puzzles.Day05
{
    public class Part1 : Puzzle<IEnumerable<BoardingInstructions>, int>
    {
        public override int SampleAnswer => 357;

        public override IEnumerable<BoardingInstructions> ParseInput(string rawInput)
            => rawInput
                .Split(Environment.NewLine)
                .Where(line => line.Length > 0)
                .Select(line => new BoardingInstructions(line));

        public override int Solve(IEnumerable<BoardingInstructions> input)
            => input.Max(b => b.SeatId);
    }
}
