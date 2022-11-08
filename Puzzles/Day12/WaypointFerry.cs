using AdventOfCode2020.Puzzles.Common;
using System;

namespace AdventOfCode2020.Puzzles.Day12
{
    public class WaypointFerry : FerryBase
    {
        Point Waypoint = new Point(0, 0);

        public WaypointFerry(Point origin, Point waypoint)
            : base(origin)
        {
            Waypoint = waypoint;
        }

        protected override void Move(NavInstruction instruction)
        {
            if (instruction.Action is 'L' or 'R')
            {
                Rotate(instruction.Action, instruction.Value);
                return;
            }

            if (instruction.Action is 'F')
            {
                for (int i = 0; i < instruction.Value; i++)
                {
                    Location = new Point(Location.X + Waypoint.X, Location.Y + Waypoint.Y);
                }

                return;
            }

            Waypoint = Move(Waypoint, GetDirection(instruction), instruction.Value);
        }

        protected override void Rotate(char turnDirection)
            => Waypoint = turnDirection switch
            {
                'L' => new Point(-1 * Waypoint.Y, Waypoint.X),
                'R' => new Point(Waypoint.Y, -1 * Waypoint.X),
                _ => throw new Exception($"turnDirection not supported: {turnDirection}")
            };
    }
}
