using System.Collections.Generic;

namespace AdventOfCode2020.Infrastructure
{
    public abstract class PuzzleBase<TInput, TAnswer>
    {
        public abstract TAnswer SampleAnswer { get; }

        public abstract TInput ParseInput(string rawInput);

        public abstract TAnswer Solve(TInput input);

        public bool ValidateSample(TInput input)
            => EqualityComparer<TAnswer>.Default.Equals(Solve(input), SampleAnswer);
    }
}
