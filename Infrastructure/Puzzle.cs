using System;
using System.Reflection;

namespace AdventOfCode2020.Infrastructure
{
    public class Puzzle<T>
    {
        public static string rootPath = @"..\";
        readonly PuzzleLocator puzzleLocator;
        readonly string sampleFile;
        readonly string inputFile;
        readonly string puzzlePath;

        public Puzzle(PuzzleLocator locator)
        {
            var a = Assembly.GetCallingAssembly();
            string day = typeof(T).Namespace.Split('.')[^1];
            puzzlePath = System.IO.Path.GetFullPath(System.IO.Path.Combine(rootPath, "Puzzles", day, "Input"));
            sampleFile = System.IO.Path.Combine(puzzlePath, "sample.txt");
            inputFile = System.IO.Path.Combine(puzzlePath, "input.txt");
            puzzleLocator = locator;
        }

        public bool ValidateSample()
        {
            var puzzleType = puzzleLocator.Get<T>();

            var instance = (T)Activator.CreateInstance(puzzleType);

            var parsedInput = GetParsedInput(sampleFile, puzzleType, instance);
            MethodInfo validate = puzzleType.GetMethod("ValidateSample");
            return (bool)validate.Invoke(instance, new[] { parsedInput });
        }

        public object Solve()
        {
            var puzzleType = puzzleLocator.Get<T>();

            var instance = (T)Activator.CreateInstance(puzzleType);

            var parsedInput = GetParsedInput(inputFile, puzzleType, instance);
            MethodInfo solve = puzzleType.GetMethod("Solve");
            return solve.Invoke(instance, new[] { parsedInput });
        }

        object GetParsedInput(string fileName, Type type, object instance)
        {
            string input = System.IO.File.ReadAllText(fileName);

            MethodInfo parseInput = type.GetMethod("ParseInput");
            return parseInput.Invoke(instance, new[] { input });
        }
    }
}
