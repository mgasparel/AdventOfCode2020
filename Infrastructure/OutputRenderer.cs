using System.Collections.Generic;
using System.Linq;
using Spectre.Console;

namespace AdventOfCode2020.Infrastructure
{
    /// <summary>
    ///     Outputs puzzle results to the Console.
    /// </summary>
    public class OutputRenderer
    {
        const string checkMark = "✔";
        const string crossMark = "❌";

        /// <summary>
        ///     Displays puzzle results in a formatted table.
        /// </summary>
        /// <param name="puzzleOutput">
        ///     A list of puzzle results to be displayed.
        /// </param>
        public static void RenderResults(List<(PuzzleOutput sample, PuzzleOutput puzzle)> puzzleOutput)
        {
            System.Console.OutputEncoding = System.Text.Encoding.UTF8;

            var table = new Table();
            table.AddColumn("Puzzle");
            table.AddColumn("Sample");
            table.AddColumn("Answer");
            table.AddColumn("Duration");
            table.AddColumn("Exception");

            table.Columns[1].Alignment(Justify.Center);

            foreach(var (sample, puzzle) in puzzleOutput.OrderBy(x => x.puzzle.Name))
            {
                var sampleResult = ((bool?)sample.Result) ?? false ? checkMark : crossMark;
                var answerColor = puzzle.Result is null ? "grey" : "blue";

                table.AddRow(
                    $"[green]{puzzle.Name}[/]",
                    sampleResult,
                    $"[{answerColor}]{(puzzle.Result?.ToString() ?? "N/A")}[/]",
                    $"{puzzle.Duration.TotalMilliseconds + sample.Duration.TotalMilliseconds}ms",
                    $"[red]{(puzzle.Exception?.Message ?? "")}[/]");
            }

            AnsiConsole.Render(table);
        }
    }
}
