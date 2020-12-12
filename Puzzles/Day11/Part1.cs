using AdventOfCode2020.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Puzzles.Day11
{
    public class Part1 : Puzzle<SeatMap, int>
    {
        public override int SampleAnswer => 37;

        protected virtual IAdjacentSeatStrategy GetAdjacentSeatStrategy(int height, int width)
            => new AdjacentSeatStrategy();

        public override SeatMap ParseInput(string rawInput)
        {
            string[] lines = rawInput
                .Split(Environment.NewLine)
                .Where(line => line.Length > 0)
                .ToArray();

            // Convert array of arrays into 2d array.
            // This simplifies cloning the array later.
            char[,] arr = new char[lines.Length, lines.First().Length];
            for (int row = 0; row < lines.Length; row++)
            {
                for (int col = 0; col < lines[row].Length; col++)
                {
                    arr[row, col] = lines[row][col];
                }
            }

            return new SeatMap(arr, GetAdjacentSeatStrategy(arr.GetLength(0) - 1, arr.GetLength(1) - 1));
        }

        public override int Solve(SeatMap input)
        {
            input.Stabilize();
            return input.OccupiedCount;
        }
    }
}
