using System.Collections.Generic;

namespace AdventOfCode2020.Infrastructure
{
    /// <summary>
    ///     Base class that all Puzzles need to implement to be discovered by the <see cref="PuzzleLocator"/>.
    /// </summary>
    /// <typeparam name="TInput">
    ///     The input type for this puzzle, after the raw input has been parsed.
    /// </typeparam>
    /// <typeparam name="TAnswer">
    ///     The return type of the answer.
    /// </typeparam>
    public abstract class Puzzle<TInput, TAnswer>
    {
        /// <summary>
        ///     If the puzzle has a sample, provide a value to this property to get feedback on whether your solution
        ///     can correctly solve the sample data.
        /// </summary>
        /// <value></value>
        public abstract TAnswer SampleAnswer { get; }

        /// <summary>
        ///     Takes the raw data from file and parses it into the format expected by your Puzzle.
        /// </summary>
        /// <param name="rawInput">
        ///     The string representation of your puzzle input.
        /// </param>
        /// <returns>
        ///     Your parsed puzzle input.
        /// </returns>
        public abstract TInput ParseInput(string rawInput);

        /// <summary>
        ///     Runs the solution on the parsed input.
        /// </summary>
        /// <param name="input">
        ///     The parsed puzzle input.
        /// </param>
        /// <returns>
        ///     The answer to the puzzle.
        /// </returns>
        public abstract TAnswer Solve(TInput input);

        /// <summary>
        ///     Checks whether your solution correctly solves your sample. See <seealso cref="SampleAnswer"/>.
        /// </summary>
        /// <param name="input">
        ///     The parsed puzzle input.
        /// </param>
        /// <returns>
        ///     Returns a value indicating whether your solution resulted in the expected sample answer.
        /// </returns>
        public virtual SampleResult ValidateSample(TInput input)
        {
            TAnswer answer = Solve(input);
            return new SampleResult(EqualityComparer<TAnswer>.Default.Equals(answer, SampleAnswer), SampleAnswer, answer);
        }
    }
}
