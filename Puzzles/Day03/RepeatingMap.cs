using AdventOfCode2020.Infrastructure;

namespace AdventOfCode2020.Puzzles.Day03
{
    public class RepeatingMap
    {
        public Point Position { get; private set; } = new Point(0, 0);
        public bool IsAtTree => lines[Position.Y][Position.X] == '#';

        string[] lines;
        int width;
        int height;

        public RepeatingMap(string[] lines)
        {
            this.lines = lines;
            this.width = lines[0].Length;
            this.height = lines.Length - 1; // make this zero-indexed.
        }

        public void Reset()
        {
            Position = new Point(0, 0);
        }

        public void Move(int x, int y)
        {
            // Can't move past the end of the map.
            if (Position.Y == height)
            {
                return;
            }

            Position = new Point((Position.X + x) % width, Position.Y + y);
        }
    }
}
