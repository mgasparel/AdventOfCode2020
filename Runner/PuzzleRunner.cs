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
            var sw = new Stopwatch();
            var cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            const string checkMark = "✅";

            // Give us 20s to run all tests, and bail after that.
            cts.CancelAfter(20_000);

            sw.Start();
            IOrderedEnumerable<Type> puzzles = PuzzleLocator.Puzzles.OrderBy(x => x.Name);
            var progressTasks = new Dictionary<string, ProgressTask>();
            var results = new Dictionary<string, (PuzzleOutput sample, PuzzleOutput puzzle)>();

            AnsiConsole.Progress()
                .AutoClear(true) // Hide list once all tasks are completed.
                .Columns(new ProgressColumn[]
                {
                    new TaskDescriptionColumn(),
                    new ProgressBarColumn()
                    {
                        IndeterminateStyle = new Style(Color.Red, Color.White)
                    },
                    new SpinnerColumn(Spinner.Known.Dots8Bit)
                })
                .StartAsync(async ctx =>
                {
                    foreach (Type puzzle in puzzles)
                    {
                        ProgressTask progressTask = ctx.AddTask(puzzle.FullName ?? "", autoStart: false);
                        progressTasks.Add(puzzle.FullName!, progressTask);
                    }

                    await Parallel.ForEachAsync(puzzles, token, async (puzzleGenericType, ct) =>
                    {
                        dynamic puzzle = PuzzleFactory.Build(puzzleGenericType);
                        string? name = puzzleGenericType?.FullName ?? "N/A";

                        ProgressTask progressTask = progressTasks[name];
                        progressTask.IsIndeterminate(true);
                        _ = results.TryAdd(
                            name,
                            (
                                await RunAsync(name, () => puzzle.ValidateSample(), token),
                                await RunAsync(name, () => puzzle.Solve(), token)
                            ));
                        progressTask.Increment(100);
                    });
                }).Wait();

            var table = new Table();
            _ = table.AddColumn("Puzzle");
            _ = table.AddColumn("Sample");
            _ = table.AddColumn("Answer");
            _ = table.AddColumn("Duration");
            _ = table.AddColumn("Exception");

            _ = table.Columns[1].Alignment(Justify.Center);

            foreach ((PuzzleOutput sample, PuzzleOutput puzzle) in results.OrderBy(x => x.Key).Select(x => x.Value))
            {
                SampleResult? sampleResult = (SampleResult?)sample?.Result ?? throw new System.Exception("SampleResult was null!");
                string sampleText = checkMark;
                if (!sampleResult.IsValid)
                {
                    sampleText = $"[red]{sampleResult.Actual}!={sampleResult.Expected}[/]";
                }

                string? answerColor = puzzle.Result is null ? "grey" : "blue";

                _ = table.AddRow(
                    $"[green]{puzzle.Name}[/]",
                    sampleText,
                    $"[{answerColor}]{puzzle.Result?.ToString() ?? "N/A"}[/]",
                    $"{puzzle.Duration.TotalMilliseconds + sample.Duration.TotalMilliseconds}ms",
                    $"[red]{puzzle.Exception?.Message ?? ""}[/]");
            }

            AnsiConsole.Write(table);
            sw.Stop();

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
