using System;
using System.Collections.Generic;
using System.Diagnostics;
using AdventOfCode2020.Infrastructure;

namespace AdventOfCode2020.Runner
{
    /// <summary>
    ///     Builds, runs, and displays the output of puzzles found by the <see cref="Infrastructure.PuzzleLocator"/>.
    /// </summary>
    public class PuzzleRunner
    {
        readonly PuzzleLocator PuzzleLocator;
        readonly PuzzleFactory PuzzleFactory;

        public PuzzleRunner(PuzzleFactory puzzleFactory, PuzzleLocator puzzleLocator)
        {
            PuzzleFactory = puzzleFactory;
            PuzzleLocator = puzzleLocator;
        }

        /// <summary>
        ///     Runs all puzzles found by the <see cref="Infrastructure.PuzzleLocator"/> and displays their output.
        /// </summary>
        public void Run()
        {
            var output = new List<(PuzzleOutput sample, PuzzleOutput puzzle)>();
            foreach (Type? puzzleGenericType in PuzzleLocator.Puzzles)
            {
                dynamic puzzle = PuzzleFactory.Build(puzzleGenericType);
                string? name = puzzleGenericType?.FullName ?? "N/A";

                output.Add(
                    (
                        Run(name, () => puzzle.ValidateSample()),
                        Run(name, () => puzzle.Solve())
                    ));
            }

            OutputRenderer.RenderResults(output);
        }

        /// <summary>
        ///     Runs a given func with error handling and timing info.
        /// </summary>
        /// <param name="puzzleName">
        ///     The name of the Puzzle.
        /// </param>
        /// <param name="func">
        ///     The method to run.
        /// </param>
        /// <returns>
        ///     A <see cref="PuzzleOutput"/> that contains the results of the Puzzle.
        /// </returns>
        static PuzzleOutput Run(string puzzleName, Func<object?> func)
        {
            var sw = new Stopwatch();
            sw.Start();

            object? retVal = null;
            Exception? caughtException = null;
            try
            {
                retVal = func();
            }
            catch (Exception e)
            {
                caughtException = e;
            }

            sw.Stop();

            return new PuzzleOutput(
                puzzleName,
                retVal,
                TimeSpan.FromMilliseconds(sw.ElapsedMilliseconds),
                caughtException);
        }
    }
}
