namespace AdventOfCode2020.Infrastructure
{
    public class SampleResult
    {
        public bool IsValid { get; }

        public object Expected { get; }

        public object Actual { get; }

        public SampleResult(bool isValid, object expected, object actual)
        {
            IsValid = isValid;
            Expected = expected;
            Actual = actual;
        }
    }
}
