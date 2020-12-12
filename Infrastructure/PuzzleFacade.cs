using System;
using System.Reflection;

namespace AdventOfCode2020.Infrastructure
{
    /// <summary>
    ///     Provides a uniform interface for puzzles with parameterless methods for validating samples and running
    ///     solutions.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PuzzleFacade<T>
    {
        public const string RootPath = @"..\";
        readonly string SampleFile;
        readonly string InputFile;
        readonly string PuzzlePath;

        public PuzzleFacade()
        {
            string day = typeof(T).Namespace!.Split('.')[^1];
            PuzzlePath = System.IO.Path.GetFullPath(System.IO.Path.Combine(RootPath, "Puzzles", day, "Input"));
            SampleFile = System.IO.Path.Combine(PuzzlePath, "sample.txt");
            InputFile = System.IO.Path.Combine(PuzzlePath, "input.txt");
        }

        /// <summary>
        ///     Validates that the solving the sample data results in the expected answer.
        /// </summary>
        /// <returns>
        ///     Returns a value indicating whether the sample answer matched the expected answer.
        /// </returns>
        public SampleResult ValidateSample()
        {
            var instance = (T)Activator.CreateInstance(typeof(T));

            object? parsedInput = PuzzleFacade<T>.GetParsedInput(SampleFile, typeof(T), instance!);
            MethodInfo? validate = typeof(T).GetMethod("ValidateSample");
            return (SampleResult)validate!.Invoke(instance, new[] { parsedInput })!;
        }

        /// <summary>
        ///     Runs the solution.
        /// </summary>
        /// <returns>
        ///     Returns the answer as an <see cref="object"/>. Since we're reflecting generic types, and we're
        ///     essentially just going to call ToString() on the result, returning a object here is the easiest thing
        ///     to do.
        /// </returns>
        public object Solve()
        {
            var instance = (T)Activator.CreateInstance(typeof(T));

            object? parsedInput = PuzzleFacade<T>.GetParsedInput(InputFile, typeof(T), instance!);
            MethodInfo? solve = typeof(T).GetMethod("Solve");
            return solve!.Invoke(instance, new[] { parsedInput })!;
        }

        static object GetParsedInput(string fileName, Type type, object instance)
        {
            string input = System.IO.File.ReadAllText(fileName);

            MethodInfo parseInput = type.GetMethod("ParseInput")!;
            return parseInput!.Invoke(instance, new[] { input })!;
        }
    }
}
