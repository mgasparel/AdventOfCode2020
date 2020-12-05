using System;
using System.Collections.Generic;
using AdventOfCode2020.Infrastructure;

namespace AdventOfCode2020.Runner
{
    public class PuzzleRunner
    {
        PuzzleLocator puzzleLocator = new ();
        PuzzleFactory puzzleFactory;

        public PuzzleRunner(PuzzleFactory puzzleFactory)
        {
            this.puzzleFactory = puzzleFactory;
        }

        public void Run()
        {
            var output = new List<(PuzzleOutput sample, PuzzleOutput puzzle)>();
            foreach(var puzzleGenericType in puzzleLocator.Puzzles)
            {
                var puzzle = puzzleFactory.Build(puzzleGenericType);
                var name = puzzleGenericType.FullName;

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

            return new PuzzleOutput(puzzleName, retVal, caughtException);
        }
    }
}
