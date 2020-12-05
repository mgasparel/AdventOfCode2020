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
        public static string rootPath = @"..\";
        readonly string sampleFile;
        readonly string inputFile;
        readonly string puzzlePath;

        public PuzzleFacade()
        {
            string day = typeof(T).Namespace!.Split('.')[^1];
            puzzlePath = System.IO.Path.GetFullPath(System.IO.Path.Combine(rootPath, "Puzzles", day, "Input"));
            sampleFile = System.IO.Path.Combine(puzzlePath, "sample.txt");
            inputFile = System.IO.Path.Combine(puzzlePath, "input.txt");
        }

        /// <summary>
        ///     Validates that the solving the sample data results in the expected answer.
        /// </summary>
        /// <returns>
        ///     Returns a value indicating whether the sample answer matched the expected answer.
        /// </returns>
        public bool ValidateSample()
        {
            var instance = (T)Activator.CreateInstance(typeof(T));

            var parsedInput = GetParsedInput(sampleFile, typeof(T), instance!);
            MethodInfo? validate = typeof(T).GetMethod("ValidateSample");
            return (bool)validate!.Invoke(instance, new[] { parsedInput })!;
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

            var parsedInput = GetParsedInput(inputFile, typeof(T), instance!);
            MethodInfo? solve = typeof(T).GetMethod("Solve");
            return solve!.Invoke(instance, new[] { parsedInput })!;
        }

        object GetParsedInput(string fileName, Type type, object instance)
        {
            string input = System.IO.File.ReadAllText(fileName);

            MethodInfo parseInput = type.GetMethod("ParseInput")!;
            return parseInput!.Invoke(instance, new[] { input })!;
        }
    }
}
