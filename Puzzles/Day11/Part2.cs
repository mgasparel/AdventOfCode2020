namespace AdventOfCode2020.Puzzles.Day11
{
    public class Part2 : Part1
    {
        public override int SampleAnswer => 26;

        protected override IAdjacentSeatStrategy GetAdjacentSeatStrategy(int height, int width)
            => new VisibleAdjacentSeatStrategy(height, width);
    }
}
