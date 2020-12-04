using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AdventOfCode2020.Infrastructure
{
    public class Puzzle<T, TAnswer>
    {
        public static string rootPath = @"..\";
        readonly Assembly puzzlesAssembly;
        static HashSet<Type> puzzles;
        readonly string sampleFile;
        readonly string inputFile;
        readonly string puzzlePath;

        public Puzzle()
        {
            string day = typeof(T).Namespace.Split('.')[^1];
            puzzlePath = System.IO.Path.GetFullPath(System.IO.Path.Combine(rootPath, "Puzzles", day, "Input"));
            sampleFile = System.IO.Path.Combine(puzzlePath, "sample.txt");
            inputFile = System.IO.Path.Combine(puzzlePath, "input.txt");

            this.puzzlesAssembly = Assembly.GetCallingAssembly();
            ReflectPuzzles();
        }

        public void ValidateSample()
        {
            puzzles.TryGetValue(typeof(T), out Type puzzleType);

            var instance = (T)Activator.CreateInstance(puzzleType);

            var parsedInput = GetParsedInput(sampleFile, puzzleType, instance);
            MethodInfo validate = puzzleType.GetMethod("ValidateSample");
            validate.Invoke(instance, new[] { parsedInput });
        }

        public TAnswer Solve()
        {
            puzzles.TryGetValue(typeof(T), out Type puzzleType);

            var instance = (T)Activator.CreateInstance(puzzleType);

            var parsedInput = GetParsedInput(inputFile, puzzleType, instance);
            MethodInfo solve = puzzleType.GetMethod("Solve");
            return (TAnswer)solve.Invoke(instance, new[] { parsedInput });
        }

        object GetParsedInput(string fileName, Type type, object instance)
        {
            string input = System.IO.File.ReadAllText(fileName);

            MethodInfo parseInput = type.GetMethod("ParseInput");
            return parseInput.Invoke(instance, new[] { input });
        }

        public void ReflectPuzzles()
        {
            puzzles = puzzlesAssembly
                .GetReferencedAssemblies()
                .Select(a => Assembly.Load(a))
                .SelectMany(x => x.DefinedTypes)
                .Where(t => InheritsGeneric(t, typeof(PuzzleBase<,>)))
                .ToHashSet<Type>();
        }

        bool InheritsGeneric(Type t, Type generic)
        {
            Type? baseType = t.BaseType;
            while(baseType is not null && baseType != typeof(object))
            {
                var typeDef = baseType.IsGenericType ? baseType.GetGenericTypeDefinition() : null;

                if (typeDef == generic)
                {
                    return true;
                }

                baseType = baseType.BaseType;
            }

            return false;
        }
    }
}
