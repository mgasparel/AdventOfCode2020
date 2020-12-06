using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Puzzles.Day06
{
    public class PlaneGroup
    {
        List<string> answers;

        public PlaneGroup(IEnumerable<string> answers)
        {
            this.answers = answers.ToList();
        }

        public int CountDistinctAnswers()
            => new HashSet<char>(answers.SelectMany(a => a))
                .Count();

        public int CountIntersectingAnswers()
            => answers.First()
                .Count(c => answers.All(a => a.Contains(c)));
    }
}
