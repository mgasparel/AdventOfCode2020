using AdventOfCode2020.Puzzles.Common;
using System.Collections.Generic;

namespace AdventOfCode2020.Puzzles.Day12
{
    public abstract class FerryBase
    {
        public Point Location { get; protected set; }

        public Direction Bearing { get; protected set; }

        protected FerryBase(Point origin)
        {
            Location = origin;
            Bearing = Direction.Right;
        }

        public void Sail(IEnumerable<NavInstruction> instructions)
        {
            foreach (NavInstruction instruction in instructions)
            {
                Move(instruction);
            }
        }

        protected Direction GetDirection(NavInstruction instruction)
            => instruction.Action switch
            {
                'N' => Direction.Up,
                'S' => Direction.Down,
                'E' => Direction.Right,
                'W' => Direction.Left,
                _ => Bearing
            };

        protected static Point Move(Point p, Direction direction, int value)
            => direction switch
            {
                Direction.Up => new Point(p.X, p.Y + value),
                Direction.Down => new Point(p.X, p.Y - value),
                Direction.Left => new Point(p.X - value, p.Y),
                Direction.Right => new Point(p.X + value, p.Y),
                _ => p
            };

        protected void Rotate(char turnDirection, int angle)
        {
            for (int i = 0; i < angle / 90; i++)
            {
                Rotate(turnDirection);
            }
        }

        protected abstract void Rotate(char turnDirection);

        protected abstract void Move(NavInstruction instruction);
    }
}
