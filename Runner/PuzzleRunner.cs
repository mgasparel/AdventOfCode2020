using System;
using System.Collections.Generic;
using System.Diagnostics;
using AdventOfCode2020.Infrastructure;

namespace AdventOfCode2020.Runner
{
    public class PuzzleRunner
    {
        readonly PuzzleLocator puzzleLocator;
        readonly PuzzleFactory puzzleFactory;

        public PuzzleRunner(PuzzleFactory puzzleFactory, PuzzleLocator puzzleLocator)
        {
            this.puzzleFactory = puzzleFactory;
            this.puzzleLocator = puzzleLocator;
        }

        public void Run()
        {
            var output = new List<(PuzzleOutput sample, PuzzleOutput puzzle)>();
            foreach(var puzzleGenericType in puzzleLocator.Puzzles)
            {
                var puzzle = puzzleFactory.Build(puzzleGenericType);
                var name = puzzleGenericType?.FullName ?? "N/A";

                output.Add(
                    (
                        Run(name, () => puzzle.ValidateSample()),
                        Run(name, () => puzzle.Solve())
                    ));
            }

            OutputRenderer.RenderResults(output);
        }

        PuzzleOutput Run(string puzzleName, Func<object?> func)
        {
            var sw = new Stopwatch();
            sw.Start();

            object? retVal = null;
            Exception? caughtException = null;
            try
            {
                retVal = func();
            }
            catch(Exception e)
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
