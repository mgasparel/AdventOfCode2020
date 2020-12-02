using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure
{
    public class Problem<T>
    {
        public string InputPath { get; }

        public Problem(string inputPath)
        {
            InputPath = inputPath;
        }

        public IEnumerable<T> Parse(Func<string, T> parseLine)
            => System.IO.File.ReadAllLines(InputPath)
                .Select(parseLine);
    }
}
