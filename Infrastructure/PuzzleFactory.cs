using System;

namespace AdventOfCode2020.Infrastructure
{
    public class PuzzleFactory
    {
        PuzzleLocator puzzleLocator;

        Type puzzleType = typeof(Puzzle<>);

        public PuzzleFactory(PuzzleLocator puzzleLocator)
        {
            this.puzzleLocator = puzzleLocator;
        }

        public dynamic Build(Type puzzleGenericType)
        {
            Type constructed = puzzleType.MakeGenericType(new Type[] { puzzleGenericType });
            dynamic instance = Activator.CreateInstance(constructed, puzzleLocator);

            if (instance is null || puzzleGenericType.FullName is null)
            {
                throw new Exception($"Error building puzzle for type: {puzzleGenericType}");
            }

            return instance;
        }
    }
}
