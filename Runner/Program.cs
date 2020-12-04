using System;
using System.Collections.Generic;
using AdventOfCode2020.Infrastructure;

namespace AdventOfCode2020.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            // var day1Part1 = new Puzzle<Puzzles.Day01.Part1, int>();
            // day1Part1.ValidateSample();
            // var result = day1Part1.Solve();

            // Console.WriteLine(result);


            // var day1Part2 = new Puzzle<Puzzles.Day01.Part2, int>();
            // day1Part2.ValidateSample();
            // var result2 = day1Part2.Solve();

            // Console.WriteLine(result2);


            var day2Part2 = new Puzzle<Puzzles.Day02.Part2, int>();
            day2Part2.ValidateSample();
            var result = day2Part2.Solve();
            Console.WriteLine(result);
        }
    }
}
