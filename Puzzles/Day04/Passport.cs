using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Puzzles.Day04
{
    public class Passport
    {
        HashSet<string> mandatory = new() { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
        string[] eyeColors = { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
        Dictionary<string, string> fields = new();

        public Passport(Dictionary<string, string> fields)
        {
            this.fields = fields;
        }

        public bool IsValid()
            => mandatory.All(x => fields.ContainsKey(x));

        public bool IsStrictValid()
            => IsValid() && fields.All(f => IsFieldValid(f.Key, f.Value));

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

        private bool IsPidValid(string value)
            => value.Length == 9
                && long.TryParse(value, out _);

        private bool IsEyeColorValid(string value)
            => eyeColors.Contains(value);

        private bool IsHairColorValid(string value)
            => value[0] == '#'
                && value.Length == 7
                && value
                    .ToLower()
                    .Skip(1)
                    .All(c => c >= '0' && c <= '9' || c>= 'a' && c <= 'z');

        private bool IsHeightValid(string value)
            => int.TryParse(value[..^2], out int iValue)
                && value[^2..] == "cm"
                    ? iValue >= 150 && iValue <= 193
                    : iValue >= 59 && iValue <= 76;

        bool IsYearValid(string value, int min, int max)
            => int.TryParse(value, out int iValue)
                && iValue >= min
                && iValue <= max;
    }
}
