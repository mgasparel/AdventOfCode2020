using System;

namespace AdventOfCode2020.Infrastructure
{
    public class PuzzleFactory
    {
        Type puzzleType = typeof(Puzzle<>);

        public dynamic Build(Type puzzleGenericType)
        {
            Type constructed = puzzleType.MakeGenericType(new Type[] { puzzleGenericType });
            dynamic instance = Activator.CreateInstance(constructed)!;

            if (instance is null || puzzleGenericType.FullName is null)
            {
                throw new Exception($"Error building puzzle for type: {puzzleGenericType}");
            }

            return instance;
        }
    }
}
