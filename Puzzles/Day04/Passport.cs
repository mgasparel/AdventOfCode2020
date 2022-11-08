using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Puzzles.Day04
{
    public class Passport
    {
        readonly HashSet<string> Mandatory = new() { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
        readonly string[] EyeColors = { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
        readonly Dictionary<string, string> Fields = new();

        public Passport(Dictionary<string, string> fields)
        {
            Fields = fields;
        }

        public bool IsValid()
            => Mandatory.All(x => Fields.ContainsKey(x));

        public bool IsStrictValid()
            => IsValid() && Fields.All(f => IsFieldValid(f.Key, f.Value));

        bool IsFieldValid(string key, string value)
            => key switch
            {
                "byr" => IsYearValid(value, 1920, 2002),
                "iyr" => IsYearValid(value, 2010, 2020),
                "eyr" => IsYearValid(value, 2020, 2030),
                "hgt" => IsHeightValid(value),
                "hcl" => IsHairColorValid(value),
                "ecl" => IsEyeColorValid(value),
                "pid" => IsPidValid(value),
                _ => true
            };

        static bool IsPidValid(string value)
            => value.Length == 9
                && long.TryParse(value, out _);

        bool IsEyeColorValid(string value)
            => EyeColors.Contains(value);

        static bool IsHairColorValid(string value)
            => value[0] == '#'
                && value.Length == 7
                && value
                    .ToLower()
                    .Skip(1)
                    .All(c => c is (>= '0' and <= '9') or (>= 'a' and <= 'z'));

        static bool IsHeightValid(string value)
            => int.TryParse(value[..^2], out int iValue)
                && value[^2..] == "cm"
                    ? iValue is >= 150 and <= 193
                    : iValue is >= 59 and <= 76;

        static bool IsYearValid(string value, int min, int max)
            => int.TryParse(value, out int iValue)
                && iValue >= min
                && iValue <= max;
    }
}
