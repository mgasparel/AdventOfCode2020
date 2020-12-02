using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AdventOfCode2020.Infrastructure
{
    public class Puzzle<T, TAnswer>
    {
        public static string rootPath = "";

        static Dictionary<Type, Assembly> puzzles = new();

        const string sampleFile = "sample.txt";
        const string puzzleFile = "puzzle.txt";

        readonly string puzzlePath;

        public Puzzle()
        {
            puzzlePath = System.IO.Path.Combine(rootPath, "Puzzles", typeof(T).Name, "Input");
        }

        public void ReflectPuzzles()
        {
            var referencedAssemblies = Assembly
                .GetCallingAssembly()
                .GetReferencedAssemblies()
                .Select(a => Assembly.Load(a));



            System.Diagnostics.Debug.Print(referencedAssemblies.Count().ToString());
        }

        public void TestSample(TAnswer expectedAnswer)
        {
            //System.Diagnostics.Debug.Assert()
        }

        // public TAnswer Solve()
        // {

        // }
    }
}
