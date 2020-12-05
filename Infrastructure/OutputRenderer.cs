using System;

namespace AdventOfCode2020.Infrastructure
{
    public class OutputRenderer
    {
        public static void RenderResults(PuzzleOutput sampleOutput, PuzzleOutput solutionOutput)
        {
            Console.WriteLine("================");
            Console.WriteLine(sampleOutput.Name);
            Console.Write("Sample Passed?: ");
            Console.WriteLine(((bool?)sampleOutput?.Result ?? false) ? "Y" : "N");
            Console.Write("Solution: ");
            Console.WriteLine(solutionOutput.Result);
        }
    }
}
