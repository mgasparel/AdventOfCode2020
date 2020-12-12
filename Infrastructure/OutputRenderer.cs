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
        const string CheckMark = "✔";

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
            _ = table.AddColumn("Puzzle");
            _ = table.AddColumn("Sample");
            _ = table.AddColumn("Answer");
            _ = table.AddColumn("Duration");
            _ = table.AddColumn("Exception");

            _ = table.Columns[1].Alignment(Justify.Center);

            foreach ((PuzzleOutput sample, PuzzleOutput puzzle) in puzzleOutput.OrderBy(x => x.puzzle.Name))
            {
                SampleResult? sampleResult = (SampleResult?)sample?.Result ?? throw new System.Exception("SampleResult was null!");
                string sampleText = CheckMark;
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

            AnsiConsole.Render(table);
        }
    }
}
