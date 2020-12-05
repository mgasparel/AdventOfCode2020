using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020.Puzzles.Day04
{
    public class Passport
    {
        HashSet<string> mandatory = new() { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };

        Dictionary<string, string> fields = new();

        string[] eyeColors = { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

        public Passport(Dictionary<string, string> fields)
        {
            this.fields = fields;
        }

        public bool IsValid()
        {
            foreach(var item in mandatory)
            {
                if (!fields.ContainsKey(item))
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsStrictValid()
        {
            if (!IsValid())
            {
                return false;
            }

            foreach (var field in fields)
            {
                if (!IsFieldValid(field.Key, field.Value))
                {
                    return false;
                }
            }

            return true;
        }

        bool IsFieldValid(string key, string value)
        {
            return key switch
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
        }

        private bool IsPidValid(string value)
        {
            if (value.Length != 9)
            {
                return false;
            }

            return long.TryParse(value, out long lValue);
        }

        private bool IsEyeColorValid(string value)
            => eyeColors.Contains(value);

        private bool IsHairColorValid(string value)
        {
            if(value[0] == '#' && value.Length == 7)
            {
                foreach(var c in value.ToLower())
                {
                    if(c < '0' && c > '9' && c < 'a' && c > 'z')
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }

        private bool IsHeightValid(string value)
        {
            if (int.TryParse(value[..^2], out int iValue))
            {
                if (value[^2..] == "cm")
                {
                    return iValue >= 150 && iValue <= 193;
                }

                if (value[^2..] == "in")
                {
                    return iValue >= 59 && iValue <= 76;
                }

                return false;
            }

            return false;
        }

        bool IsYearValid(string value, int min, int max)
        {
            if (int.TryParse(value, out int iValue))
            {
                return iValue >= min && iValue <= max;
            }

            return false;
        }
    }
}
