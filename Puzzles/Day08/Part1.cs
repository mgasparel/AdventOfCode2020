using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2020.Infrastructure;

namespace AdventOfCode2020.Puzzles.Day08
{
    public class Part1 : Puzzle<IEnumerable<Instruction>, int>
    {
        public override int SampleAnswer => 5;

        public override IEnumerable<Instruction> ParseInput(string rawInput)
            => rawInput
                .Split(Environment.NewLine)
                .Where(line => line.Length > 0)
                .Select(ParseInstruction);

        Instruction ParseInstruction(string line)
        {
            string op = line[..3];
            int.TryParse(line[4..], out int val);

            return new Instruction(op, val);
        }

        public override int Solve(IEnumerable<Instruction> input)
        {
            var bootSeq = new BootSequence();
            bootSeq.Run(input.ToArray());
            return bootSeq.Accumulator;
        }
    }
}
