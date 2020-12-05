using System;
using System.Collections.Generic;
using Spectre.Console;

namespace AdventOfCode2020.Infrastructure
{
    public class OutputRenderer
    {
        const string checkMark = "[green]✔[/]";
        const string crossMark = "❌";

        public static void RenderResults(List<(PuzzleOutput sample, PuzzleOutput puzzle)> puzzleOutput)
        {
            System.Console.OutputEncoding = System.Text.Encoding.UTF8;

            var table = new Table();
            table.AddColumn("Puzzle");
            table.AddColumn("Sample");
            table.AddColumn("Answer");
            table.AddColumn("Exception");

            table.Columns[1].Alignment(Justify.Center);

            foreach(var (sample, puzzle) in puzzleOutput)
            {
                var sampleResult = ((bool?)sample.Result) ?? false ? checkMark : crossMark;
                var answerColor = puzzle.Result is null ? "grey" : "blue";

                table.AddRow(
                    $"[green]{puzzle.Name}[/]",
                    sampleResult,
                    $"[{answerColor}]{(puzzle.Result?.ToString() ?? "N/A")}[/]",
                    $"[red]{(puzzle.Exception?.Message ?? "")}[/]");
            }

            AnsiConsole.Render(table);
        }
    }
}
