using System.Collections.Generic;

namespace AdventOfCode2020.Infrastructure
{
    public interface IPuzzle<TInput, TOutput>
    {
        TOutput SampleAnswer { get; }

        TInput ParseInput(string rawInput);

        TOutput Solve(TInput input);

        void ValidateSample(TInput input);
    }
}
