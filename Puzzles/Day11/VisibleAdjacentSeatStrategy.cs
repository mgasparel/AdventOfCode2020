using System.Collections.Generic;

namespace AdventOfCode2020.Puzzles.Day11
{
    public class VisibleAdjacentSeatStrategy : IAdjacentSeatStrategy
    {
        static readonly (int, int)[] Slopes = new (int, int)[]
        {
            (-1, 0), (1, 0), (0, -1), (0, 1), (-1, -1), (-1, 1), (1, 1), (1, -1)
        };

        readonly int Width;
        readonly int Height;

        public int AdjacentThreshold => 5;

        public VisibleAdjacentSeatStrategy(int height, int width)
        {
            Width = width;
            Height = height;
        }

        public IEnumerable<(int row, int col)> FindAdjacentSeats(int row, int col, (char Current, char? Next)[,] seats)
        {
            foreach ((int incRow, int incCol) in Slopes)
            {
                (int row, int col)? occupied = OccupiedSeatAtSlope(row, col, incRow, incCol, seats);
                if (occupied is not null)
                {
                    yield return occupied.Value;
                }
            }
        }

        (int row, int col)? OccupiedSeatAtSlope(int startRow, int startCol, int incrementRow, int incrementCol, (char Current, char? Next)[,] seats)
        {
            int rowToCheck = startRow + incrementRow;
            int colToCheck = startCol + incrementCol;

            while (IsInBounds(rowToCheck, colToCheck))
            {
                if (seats[rowToCheck, colToCheck].Current == SeatMap.Occupied)
                {
                    return (rowToCheck, colToCheck);
                }

                if (seats[rowToCheck, colToCheck].Current == SeatMap.Empty)
                {
                    return null;
                }

                rowToCheck += incrementRow;
                colToCheck += incrementCol;
            }

            return null;
        }

        bool IsInBounds(int row, int col)
            => row >= 0
                && row <= Height
                && col >= 0
                && col <= Width;
    }
}
