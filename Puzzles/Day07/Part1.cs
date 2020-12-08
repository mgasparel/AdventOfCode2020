using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2020.Infrastructure;
using AdventOfCode2020.Puzzles.Common;

namespace AdventOfCode2020.Puzzles.Day07
{
    public class Part1 : Puzzle<Dictionary<string, Node<Bag>>, long>
    {
        protected const string Target = "shiny gold";

        public override long SampleAnswer => 4;

        public override Dictionary<string, Node<Bag>> ParseInput(string rawInput)
            => rawInput
                .Split(Environment.NewLine)
                .Where(line => line.Length > 0)
                .Select(ParseBagDescription)
                .ToDictionary(node => node.Name, node => node);

        Node<Bag> ParseBagDescription(string description)
        {
            var parts = description.Split(" bags contain ");
            var name = parts[0];

            var node = new Node<Bag>(name, new Bag(name));

            var innerBagNodes = parts[1]
                .Split(',')
                .Where(description => description != "no other bags.")
                .Select(bag => bag.TrimStart())
                .Select(bag => ParseBagContents(bag))
                .Select(bag => new Node<Bag>(bag.Name, bag));

            node.AddChildren(innerBagNodes);

            return node;
        }

        Bag ParseBagContents(string contents)
        {
            int space = contents.IndexOf(' ');
            string name = contents[(space + 1)..contents.LastIndexOf(' ')];
            int.TryParse(contents[..space], out int count);

            return new Bag(name, count);
        }

        public override long Solve(Dictionary<string, Node<Bag>> input)
            => input.Count(x => HasDescendent(input, Target, x.Value));

        bool HasDescendent(Dictionary<string, Node<Bag>> allNodes, string name, Node<Bag> node)
            => node.Children.Any(n => n.Name == Target || HasDescendent(allNodes, name, allNodes[n.Name]));
    }
}
