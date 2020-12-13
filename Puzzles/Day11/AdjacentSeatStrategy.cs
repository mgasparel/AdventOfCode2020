using System.Collections.Generic;

namespace AdventOfCode2020.Puzzles.Day11
{
    public class AdjacentSeatStrategy : IAdjacentSeatStrategy
    {
        public int AdjacentThreshold { get; } = 4;

        static IEnumerable<int> AdjacentSeatIndexes
        {
            get
            {
                for (int i = 0; i < 9; i++)
                {
                    if (i == 4)
                    {
                        continue; // skip self
                    }

                    yield return i;
                }
            }
        }

        public IEnumerable<(int row, int col)> FindAdjacentSeats(int row, int col, (char Current, char? Next)[,] seats)
        {
            foreach (int i in AdjacentSeatIndexes)
            {
                // row/col relative to the coordinates passed in.
                int c = (i % 3) - 1;
                int r = (i / 3) - 1;

                yield return (row + r, col + c);
            }
        }
    }
}
