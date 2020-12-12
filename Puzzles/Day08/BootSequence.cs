using System.Collections.Generic;

namespace AdventOfCode2020.Puzzles.Day08
{
    public class BootSequence
    {
        readonly List<int> History = new();

        public int Accumulator { get; private set; }

        public bool Run(Instruction[] instructions)
        {
            int i = 0;
            while (i < instructions.Length)
            {
                if (History.Contains(i))
                {
                    return false;
                }

                switch (instructions[i].Operation)
                {
                    case "nop":
                        History.Add(i);
                        i++;
                        break;
                    case "acc":
                        History.Add(i);
                        Accumulator += instructions[i].Value;
                        i++;
                        break;
                    case "jmp":
                        History.Add(i);
                        i += instructions[i].Value;
                        break;
                    default:
                        break;
                }
            }

            return true;
        }
    }
}
