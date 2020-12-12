using System;
using System.Linq;

namespace AdventOfCode2020.Puzzles.Day11
{
    public class SeatMap
    {
        public static char Empty = 'L';
        public static char Occupied = '#';
        public static char Floor = '.';
        readonly (char Current, char? Next)[,] Seats;
        readonly int Height;
        readonly int Width;

        readonly IAdjacentSeatStrategy AdjacentSeatStrategy;

        public int OccupiedCount {
            get {
                int count = 0;
                for (int row = 0; row < Seats.GetLength(0); row++)
                {
                    for (int col = 0; col < Seats.GetLength(1); col++)
                    {
                        count += Seats[row, col].Current == Occupied ? 1 : 0;
                    }
                }

                return count;
            }
        }

        public SeatMap(char[,] seats, IAdjacentSeatStrategy adjacentSeatLocator)
        {
            AdjacentSeatStrategy = adjacentSeatLocator;
            Height = seats.GetLength(0) - 1;
            Width = seats.GetLength(1) - 1;

            Seats = new (char Current, char? Next)[seats.GetLength(0), seats.GetLength(1)];
            for (int row = 0; row < Seats.GetLength(0); row++)
            {
                for (int col = 0; col < Seats.GetLength(1); col++)
                {
                    Seats[row, col] = (seats[row, col], null);
                }
            }
        }

        public void Stabilize()
        {
            // Apply the rules until no more changes occur.
            while (ApplyRulesToNext())
            {
                CycleSeatMap();
            }

            return;
        }

        void CycleSeatMap()
        {
            for (int row = 0; row <= Height; row++)
            {
                for (int col = 0; col <= Width; col++)
                {
                    (_, char? next) = Seats[row, col];
                    if (next != null)
                    {
                        Seats[row, col].Current = next!.Value;
                        Seats[row, col].Next = null;
                    }
                }
            }
        }

        bool ApplyRulesToNext()
        {
            bool changed = false;

            for (int r = 0; r <= Height; r++)
            {
                for (int c = 0; c <= Width; c++)
                {
                    char seat = ApplyRules(r, c);
                    if (Seats[r, c].Current != seat)
                    {
                        Seats[r, c].Next = seat; // update our copy of the seat map with the updated state.
                        changed = true;
                    }
                }
            }

            return changed;
        }

        // Apply the rules to the seat at the coordinates, and return the new state of that seat.
        char ApplyRules(int row, int col)
        {
            int adjacentOccupied = AdjacentSeats(row, col)
                .Count(seat => seat == Occupied);

            if (Seats[row, col].Current == Empty && adjacentOccupied == 0)
            {
                return Occupied;
            }

            if (Seats[row, col].Current == Occupied && adjacentOccupied >= AdjacentSeatStrategy.AdjacentThreshold)
            {
                return Empty;
            }

            return Seats[row, col].Current;
        }

        char?[] AdjacentSeats(int row, int col)
            => AdjacentSeatStrategy
                .FindAdjacentSeats(row, col, Seats)
                .Select(s => GetSeat(s.row, s.col))
                .ToArray();

        protected char? GetSeat(int row, int col)
            => row < 0 || col < 0 || row > Height || col > Width
                ? null
                : Seats[row, col].Current;
    }
}
