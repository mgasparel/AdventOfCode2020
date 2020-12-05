using System;
using System.Reflection;

namespace AdventOfCode2020.Infrastructure
{
    public class Puzzle<T>
    {
        public static string rootPath = @"..\";
        readonly string sampleFile;
        readonly string inputFile;
        readonly string puzzlePath;

        public Puzzle()
        {
            string day = typeof(T).Namespace!.Split('.')[^1];
            puzzlePath = System.IO.Path.GetFullPath(System.IO.Path.Combine(rootPath, "Puzzles", day, "Input"));
            sampleFile = System.IO.Path.Combine(puzzlePath, "sample.txt");
            inputFile = System.IO.Path.Combine(puzzlePath, "input.txt");
        }

        public bool ValidateSample()
        {
            var instance = (T)Activator.CreateInstance(typeof(T));

            var parsedInput = GetParsedInput(sampleFile, typeof(T), instance!);
            MethodInfo? validate = typeof(T).GetMethod("ValidateSample");
            return (bool)validate!.Invoke(instance, new[] { parsedInput })!;
        }

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
