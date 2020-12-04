using System.Collections.Generic;

namespace AdventOfCode2020.Infrastructure
{
    public abstract class PuzzleBase<TInput, TAnswer>
    {
        public abstract TAnswer SampleAnswer { get; }

        public abstract TInput ParseInput(string rawInput);

        public abstract TAnswer Solve(TInput input);

        public void ValidateSample(TInput input)
        {
            TAnswer result = Solve(input);

            System.Diagnostics.Debug.Assert(EqualityComparer<TAnswer>.Default.Equals(result, SampleAnswer));
        }
    }
}
