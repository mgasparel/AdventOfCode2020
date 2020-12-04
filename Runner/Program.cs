using System;
using System.Reflection;
using AdventOfCode2020.Infrastructure;

PuzzleLocator puzzleLocator = new (Assembly.GetExecutingAssembly());
Type puzzleType = typeof(Puzzle<>);

foreach(var puzzleGenericType in puzzleLocator.Puzzles)
{
    ReflectedPuzzle puzzle = BuildPuzzle(puzzleGenericType);

    RenderResults(Run(puzzle, "ValidateSample"), Run(puzzle, "Solve"));
}

ReflectedPuzzle BuildPuzzle(Type puzzleGenericType)
{
    Type constructed = puzzleType.MakeGenericType(new Type[] { puzzleGenericType });
    object? instance = Activator.CreateInstance(constructed, puzzleLocator);

    if (instance is null || puzzleGenericType.FullName is null)
    {
        throw new Exception($"Error building puzzle for type: {puzzleGenericType}");
    }

    return new ReflectedPuzzle(puzzleGenericType.FullName, constructed, instance);
}

PuzzleOutput Run(ReflectedPuzzle puzzle, string method)
{
    object? retVal = null;
    Exception? caughtException = null;
    try
    {
        retVal = CallMethod(puzzle, method);
    }
    catch(Exception e)
    {
        caughtException = e;
    }

    return new PuzzleOutput(puzzle.Name, retVal, caughtException);
}

object? CallMethod(ReflectedPuzzle puzzle, string method)
    => puzzle.GenericPuzzle
            .GetMethod(method)
            ?.Invoke(puzzle.Instance, null);

void RenderResults(PuzzleOutput sampleOutput, PuzzleOutput solutionOutput)
{
    Console.WriteLine("================");
    Console.WriteLine(sampleOutput.Name);
    Console.Write("Sample Passed?: ");
    Console.WriteLine(((bool?)sampleOutput?.Result ?? false) ? "Y" : "N");
    Console.Write("Solution: ");
    Console.WriteLine(solutionOutput.Result);
}

public record ReflectedPuzzle(string Name, Type GenericPuzzle, object Instance);
public record PuzzleOutput(string Name, object? Result, Exception? Exception);
