using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AdventOfCode2020.Infrastructure;
using Spectre.Console;

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
            var output = new ConcurrentBag<(PuzzleOutput sample, PuzzleOutput puzzle)>();
            var sw = new Stopwatch();
            var cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;

            // Give us 20s to run all tests, and bail after that.
            cts.CancelAfter(20_000);

            int completed = 0;
            var progressTask = Task.Run(() =>
                AnsiConsole.Progress()
                    .AutoClear(true)
                    .Columns(new ProgressColumn[]
                    {
                        new TaskDescriptionColumn(),    // Task description
                        new ProgressBarColumn(),        // Progress bar
                        new SpinnerColumn(),            // Spinner
                    })
                    .Start(ctx =>
                    {
                        ProgressTask task1 = ctx.AddTask("[blue]Vacationing in the tropics[/]");
                        task1.MaxValue = PuzzleLocator.Puzzles.Count - 100;
                        task1.StartTask();
                        while (!ctx.IsFinished && !token.IsCancellationRequested)
                        {
                            double increment = completed - task1.Value;
                            task1.Increment(increment);
                        }
                    }), token);

            sw.Start();
            _ = PuzzleLocator.Puzzles.ParallelForEachAsync(async (puzzleGenericType) =>
            {
                dynamic puzzle = PuzzleFactory.Build(puzzleGenericType);
                string? name = puzzleGenericType?.FullName ?? "N/A";

                output.Add(
                    (
                        await RunAsync(name, () => puzzle.ValidateSample(), token),
                        await RunAsync(name, () => puzzle.Solve(), token)
                    ));

                Interlocked.Increment(ref completed);
            });
            sw.Stop();

            progressTask.GetAwaiter().GetResult();

            OutputRenderer.RenderResults(output);

            AnsiConsole.Console.MarkupLine($"[yellow]Advent of Code 2020 - Total Run Time: [/][teal]{sw.ElapsedMilliseconds}ms[/]");
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
        static async Task<PuzzleOutput> RunAsync(string puzzleName, Func<object?> func, CancellationToken cancellationToken)
        {
            var sw = new Stopwatch();
            sw.Start();

            object? retVal = null;
            Exception? caughtException = null;
            try
            {
                var t = Task.Run(() => func(), cancellationToken);
                retVal = await t;
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
