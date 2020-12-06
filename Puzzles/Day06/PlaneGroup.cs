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
        {
            var h = new HashSet<char>();

            foreach(var form in answers)
            {
                foreach(var response in form)
                {
                    h.Add(response);
                }
            }

            return h.Count();
        }

        public int CountIntersectingAnswers()
        {
            if (answers.Count == 1)
            {
                return answers[0].Length;
            }

            int intersectCount = 0;
            foreach(var c in answers[IndexOfShortestElement()])
            {
                if(answers.All(a => a.Contains(c)))
                {
                    intersectCount++;
                }
            }

            return intersectCount;
        }

        int IndexOfShortestElement()
        {
            int shortestElementIndex = 0;
            answers
                .Select((answer, index) => (answer, index))
                .Min(x =>
                    {
                        shortestElementIndex = x.index;
                        return x.answer.Length;
                    });

            return shortestElementIndex;
        }
    }
}
