using AdventOfCode2020.Puzzles.Common;

namespace AdventOfCode2020.Puzzles.Day12
{
    public class Ferry : FerryBase
    {
        public Ferry(Point origin)
            : base(origin)
        {
        }

        protected override void Rotate(char turnDirection)
        {
            int iBearing = (int)Bearing;
            Bearing = turnDirection switch {
                'L' => (Direction)((iBearing + 3) % 4),
                'R' => (Direction)((iBearing + 1) % 4),
                _ => Bearing
            };
        }

        protected override void Move(NavInstruction instruction)
        {
            if (instruction.Action is 'L' or 'R')
            {
                Rotate(instruction.Action, instruction.Value);
                return;
            }

            Move(GetDirection(instruction), instruction.Value);
        }

        void Move(Direction direction, int value)
            => Location = Move(Location, direction, value);
    }
}
