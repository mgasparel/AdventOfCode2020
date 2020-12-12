using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2020.Infrastructure
{
    public static class IEnumerableExtensions
    {
        public static Task ParallelForEachAsync<T>(this IEnumerable<T> source, int dop, Func<T, Task> body)
        {
            async Task AwaitPartition(IEnumerator<T> partition)
            {
                using (partition)
                {
                    while (partition.MoveNext())
                    { await body(partition.Current); }
                }
            }
            return Task.WhenAll(
                Partitioner
                    .Create(source)
                    .GetPartitions(dop)
                    .AsParallel()
                    .Select(p => AwaitPartition(p)));
        }

        public static Task ParallelForEachAsync<T>(this IEnumerable<T> source, Func<T, Task> body)
            => ParallelForEachAsync(source, Environment.ProcessorCount, body);
    }
}
