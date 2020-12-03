using System;
using System.Collections.Generic;
using AdventOfCode2020.Infrastructure;

namespace AdventOfCode2020.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            var day1Part1 = new Puzzle<Puzzles.Day01.Part1, IEnumerable<int>, int>();
            day1Part1.ValidateSample();
            var result = day1Part1.Solve();

            Console.WriteLine(result);
        }
    }
}
