using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AdventOfCode2020.Infrastructure
{
    public class PuzzleLocator
    {
        public HashSet<Type> Puzzles;

        public PuzzleLocator()
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
