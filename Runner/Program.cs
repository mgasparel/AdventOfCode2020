using System;
using System.Reflection;
using AdventOfCode2020.Infrastructure;

namespace AdventOfCode2020.Runner
{
    public record ReflectedPuzzle(Type GenericPuzzle, object Instance);

    class Program
    {
        static PuzzleLocator puzzleLocator = new (Assembly.GetExecutingAssembly());
        static Type puzzleType = typeof(Puzzle<>);

        static void Main(string[] args)
        {
            foreach(var puzzleGenericType in puzzleLocator.Puzzles)
            {
                Console.WriteLine($"Running puzzle: {puzzleGenericType.FullName}");

                ReflectedPuzzle puzzle = BuildPuzzle(puzzleGenericType);

                RunSample(puzzle);

                object answer = RunSolution(puzzle);

                Console.WriteLine(answer);
            }
        }

        static ReflectedPuzzle BuildPuzzle(Type puzzleGenericType)
        {
            Type constructed = puzzleType.MakeGenericType(new Type[] { puzzleGenericType });
            object instance = Activator.CreateInstance(constructed, puzzleLocator);

            return new ReflectedPuzzle(constructed, instance);
        }

        static void RunSample(ReflectedPuzzle puzzle)
        {
            try
            {
                CallMethod(puzzle, "ValidateSample");
                Console.WriteLine("Sample validated");
            }
            catch(Exception)
            {
                Console.WriteLine("Sample failed to validate!");
            }
        }

        static object? RunSolution(ReflectedPuzzle puzzle)
        {
            object? retVal = null;
            try
            {
                retVal = CallMethod(puzzle, "Solve");
            }
            catch(Exception e)
            {
                Console.WriteLine($"Exception during solution execution: {e.Message}");
                Console.WriteLine(e.StackTrace);
            }

            return retVal;
        }

        static object? CallMethod(ReflectedPuzzle puzzle, string method)
            => puzzle.GenericPuzzle
                    .GetMethod(method)
                    .Invoke(puzzle.Instance, null);

    }
}
