using System;

namespace AdventOfCode2020.Infrastructure
{
    public record PuzzleOutput(string Name, object? Result, TimeSpan Duration, Exception? Exception);
}
