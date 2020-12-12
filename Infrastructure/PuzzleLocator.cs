using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AdventOfCode2020.Infrastructure
{
    /// <summary>
    ///     This class is responsible for finding all tests that inherit from <see cref="Puzzle{TInput, TAnswer}"/>.
    /// </summary>
    public class PuzzleLocator
    {
        public HashSet<Type> Puzzles { get; }

        public PuzzleLocator()
        {
            Puzzles ??= System.IO.Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
                .Select(x => Assembly.Load(AssemblyName.GetAssemblyName(x)))
                .SelectMany(x => x.DefinedTypes)
                .Where(t => InheritsGeneric(t, typeof(Puzzle<,>)))
                .ToHashSet<Type>();
        }

        static bool InheritsGeneric(Type t, Type generic)
        {
            Type? baseType = t.BaseType;
            while (baseType is not null && baseType != typeof(object))
            {
                Type? typeDef = baseType.IsGenericType ? baseType.GetGenericTypeDefinition() : null;

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
