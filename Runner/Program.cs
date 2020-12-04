using System;
using System.Reflection;
using AdventOfCode2020.Infrastructure;

namespace AdventOfCode2020.Runner
{
    public record ReflectedPuzzle(string Name, Type GenericPuzzle, object Instance);
    public record PuzzleOutput(string Name, object? Result, Exception? Exception);

    class Program
    {
        static PuzzleLocator puzzleLocator = new (Assembly.GetExecutingAssembly());
        static Type puzzleType = typeof(Puzzle<>);

        static void Main(string[] args)
        {
            foreach(var puzzleGenericType in puzzleLocator.Puzzles)
            {
                ReflectedPuzzle puzzle = BuildPuzzle(puzzleGenericType);

                RenderResults(Run(puzzle, "ValidateSample"), Run(puzzle, "Solve"));
            }
        }

        static ReflectedPuzzle BuildPuzzle(Type puzzleGenericType)
        {
            Type constructed = puzzleType.MakeGenericType(new Type[] { puzzleGenericType });
            object instance = Activator.CreateInstance(constructed, puzzleLocator);

            return new ReflectedPuzzle(puzzleGenericType.FullName, constructed, instance);
        }

        static PuzzleOutput Run(ReflectedPuzzle puzzle, string method)
        {
            object? retVal = null;
            Exception? caughtException = null;
            try
            {
                retVal = CallMethod(puzzle, method);
            }
            catch(Exception e)
            {
                caughtException = e;
            }

            return new PuzzleOutput(puzzle.Name, retVal, caughtException);
        }

        static object? CallMethod(ReflectedPuzzle puzzle, string method)
            => puzzle.GenericPuzzle
                    .GetMethod(method)
                    .Invoke(puzzle.Instance, null);

        static void RenderResults(PuzzleOutput sampleOutput, PuzzleOutput solutionOutput)
        {
            Console.WriteLine("================");
            Console.WriteLine(sampleOutput.Name);
            Console.Write("Sample Passed?: ");
            Console.WriteLine(((bool?)sampleOutput.Result) ?? false ? "Y" : "N");
            Console.Write("Solution: ");
            Console.WriteLine(solutionOutput.Result);
        }

    }
}
