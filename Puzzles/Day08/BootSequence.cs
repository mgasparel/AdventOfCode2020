using System.Collections.Generic;

namespace AdventOfCode2020.Puzzles.Day08
{
    public class BootSequence
    {
        List<int> history = new();

        public int Accumulator { get; private set; }

        public bool Run(Instruction[] instructions)
        {
            int i = 0;
            while(i < instructions.Length)
            {
                if (history.Contains(i))
                {
                    return false;
                }

                switch (instructions[i].Operation)
                {
                    case "nop":
                        history.Add(i);
                        i++;
                        break;
                    case "acc":
                        history.Add(i);
                        Accumulator += instructions[i].Value;
                        i++;
                        break;
                    case "jmp":
                        history.Add(i);
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
