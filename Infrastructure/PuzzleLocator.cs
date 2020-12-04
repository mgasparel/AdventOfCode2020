using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AdventOfCode2020.Infrastructure
{
    public class PuzzleLocator
    {
        Assembly puzzlesAssembly;
        public HashSet<Type> Puzzles;

        public PuzzleLocator(Assembly puzzlesAssembly)
        {
            this.puzzlesAssembly = puzzlesAssembly;

            ReflectPuzzles();
        }

        public Type Get<T>()
        {
            Puzzles.TryGetValue(typeof(T), out Type puzzleType);
            return puzzleType;
        }

        public Type Get(Type type)
        {
            Puzzles.TryGetValue(type, out Type puzzleType);
            return puzzleType;
        }

        public void ReflectPuzzles()
        {
            Puzzles ??= System.IO.Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
                .Select(x => Assembly.Load(AssemblyName.GetAssemblyName(x)))
                .SelectMany(x => x.DefinedTypes)
                .Where(t => InheritsGeneric(t, typeof(PuzzleBase<,>)))
                .ToHashSet<Type>();
        }

        static bool InheritsGeneric(Type t, Type generic)
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
