using System.Collections.Generic;

namespace AdventOfCode2020.Puzzles.Day11
{
    public interface IAdjacentSeatStrategy
    {
        int AdjacentThreshold { get; }

        IEnumerable<(int row, int col)> FindAdjacentSeats(int row, int col, (char Current, char? Next)[,] seats);
    }
}
