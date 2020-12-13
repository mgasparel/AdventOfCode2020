using AdventOfCode2020.Infrastructure;
using System;
using System.Collections;
using System.Linq;

namespace AdventOfCode2020.Puzzles.Day13
{
    public class Part1 : Puzzle<BusSchedule, int>
    {
        public override int SampleAnswer => 295;

        public override BusSchedule ParseInput(string rawInput)
        {
            string[] lines = rawInput.Split(Environment.NewLine);

            return new BusSchedule(
                int.Parse(lines[0]),
                lines[1]
                    .Split(',')
                    .Where(val => val != "x")
                    .Select(val => int.Parse(val)));
        }

        public override int Solve(BusSchedule input)
        {
            (int busId, int wait) = input.BusIds
                .Select(id => NextBusArrival(input.Now, id))
                .OrderBy(bus => bus.wait)
                .First();

            return busId * wait;
        }

        static (int busId, int wait) NextBusArrival(int currentTime, int busId)
            => (busId, (currentTime / busId * busId) - currentTime + busId);
    }
}
