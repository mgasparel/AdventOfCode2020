using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Puzzles.Day06
{
    public class PlaneGroup
    {
        readonly List<string> Answers;

        public PlaneGroup(IEnumerable<string> answers)
        {
            Answers = answers.ToList();
        }

        public int CountDistinctAnswers()
            => new HashSet<char>(Answers.SelectMany(a => a))
                .Count;

        public int CountIntersectingAnswers()
            => Answers.First()
                .Count(c => Answers.All(a => a.Contains(c)));
    }
}
