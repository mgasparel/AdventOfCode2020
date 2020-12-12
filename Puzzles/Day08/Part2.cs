using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Puzzles.Day08
{
    public class Part2 : Part1
    {
        public override int SampleAnswer => 8;

        public override int Solve(IEnumerable<Instruction> input)
        {
            int count = input.Count();
            for (int i = 0; i < count; i++)
            {
                Instruction[]? aInput = input.ToArray();
                var bootSeq = new BootSequence();

                SwapOp(ref aInput[i]);

                if (bootSeq.Run(aInput))
                {
                    return bootSeq.Accumulator;
                };
            }

            throw new Exception("no solution found!");
        }

        static void SwapOp(ref Instruction instruction)
            => instruction = instruction with { Operation = instruction.Operation == "nop" ? "jmp" : "nop" };
    }
}
