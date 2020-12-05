using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Puzzles.Day05
{
    public class BoardingInstructions
    {
        string columnInstructions { get; }
        string rowInstructions { get; }
        IEnumerable<int> rows = Enumerable.Range(0, 128);
        IEnumerable<int> cols = Enumerable.Range(0, 8);
        public int SeatId => LocateRow() * 8 + LocateColumn();

        public BoardingInstructions(string instructions)
        {
            columnInstructions = instructions[^3..];
            rowInstructions = instructions[..7];
        }

        int LocateRow()
            => Locate(rows, rowInstructions, 'B');

        int LocateColumn()
            => Locate(cols, columnInstructions, 'R');

        int Locate(IEnumerable<int> items, string instructions, char upperIndicator)
        {
            var current = items;
            var size = items.Count();
            foreach(var c in instructions)
            {
                size = size / 2;

                if (c == upperIndicator)
                {
                    current = current.Skip(size);
                }

                current = current.Take(size);
            }

            return current.First();
        }
    }
}
