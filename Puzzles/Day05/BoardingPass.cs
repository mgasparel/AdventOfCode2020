using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Puzzles.Day05
{
    public class BoardingInstructions
    {
        string ColumnInstructions { get; }
        string RowInstructions { get; }

        readonly IEnumerable<int> Rows = Enumerable.Range(0, 128);
        readonly IEnumerable<int> Cols = Enumerable.Range(0, 8);
        public int SeatId => (LocateRow() * 8) + LocateColumn();

        public BoardingInstructions(string instructions)
        {
            ColumnInstructions = instructions[^3..];
            RowInstructions = instructions[..7];
        }

        int LocateRow()
            => Locate(Rows, RowInstructions, 'B');

        int LocateColumn()
            => Locate(Cols, ColumnInstructions, 'R');

        static int Locate(IEnumerable<int> items, string instructions, char upperIndicator)
        {
            IEnumerable<int>? current = items;
            int size = items.Count();
            foreach (char c in instructions)
            {
                size /= 2;

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
