using AdventOfCode2020.Puzzles.Common;

namespace AdventOfCode2020.Puzzles.Day03
{
    public class RepeatingMap
    {
        public Point Position { get; private set; } = new Point(0, 0);
        public bool IsAtTree => Lines[Position.Y][Position.X] == '#';

        readonly string[] Lines;
        readonly int Width;
        readonly int Height;

        public RepeatingMap(string[] lines)
        {
            Lines = lines;
            Width = lines[0].Length;
            Height = lines.Length - 1; // make this zero-indexed.
        }

        public void Reset() => Position = new Point(0, 0);

        public void Move(int x, int y)
        {
            // Can't move past the end of the map.
            if (Position.Y == Height)
            {
                return;
            }

            Position = new Point((Position.X + x) % Width, Position.Y + y);
        }
    }
}
