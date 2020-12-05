using System;

namespace AdventOfCode2020.Infrastructure
{
    /// <summary>
    ///     Given a puzzle type, this class instantiates a new instance of <see cref="PuzzleFacade"/> for that type.
    /// </summary>
    public class PuzzleFactory
    {
        Type puzzleType = typeof(PuzzleFacade<>);

        /// <summary>
        ///     Create a new instance of <see cref="PuzzleFacade"/> using the generic arguments provided.
        /// </summary>
        /// <param name="puzzleGenericType">
        ///     The puzzle type to build. Should inherit from <see cref="Puzzle{T, T}"/>.
        /// </param>
        /// <returns>
        ///     A new instance of <see cref="PuzzleFacade"/>.
        /// </returns>
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
