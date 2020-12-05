using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Puzzles.Day05
{
    public class Part2 : Part1
    {
        public override int Solve(IEnumerable<BoardingInstructions> input)
        {
            var seats = input.Select(x => x.SeatId).OrderBy(x => x).ToArray();

            for(int i = 0; i < seats.Length; i++)
            {
                // Ignore front and back of plane.
                if(i == 0 || i == seats.Length)
                {
                    continue;
                }

                if(seats[i] + 1 != seats[i + 1])
                {
                    return seats[i] + 1;
                }
            }

            return 0;
        }

        // No sample provided.
        public override bool ValidateSample(IEnumerable<BoardingInstructions> input) => true;
    }
}

