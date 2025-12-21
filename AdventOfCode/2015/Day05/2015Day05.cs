using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class _2015Day05 : _2015Day
    {
        private readonly List<string> words;

        public _2015Day05() : base("Day05")
        {
            words = Input.ToList();
        }

        public override void Part1()
        {
            base.Part1();

            int niceCount = 0;

            foreach (string word in words)
            {
                bool doubleLetter = Regex.IsMatch(word, @"(.)\1");
                bool threeVovel = Regex.IsMatch(word, @"[aeiou].*[aeiou].*[aeiou]");
                bool forbidden = Regex.IsMatch(word, @"ab|cd|pq|xy");

                if (doubleLetter && threeVovel & !forbidden)
                {
                    niceCount++;
                }
            }

            Console.WriteLine($"Number of nice words: {niceCount}");
        }

        public override void Part2()
        {
            base.Part2();

            int niceCount = 0;

            foreach (string word in words)
            {
                bool doubleGroup = Regex.IsMatch(word, @"(..).*\1");
                bool triplet = Regex.IsMatch(word, @"(.).\1");

                if (doubleGroup && triplet)
                {
                    niceCount++;
                }
            }

            Console.WriteLine($"Number of nice words: {niceCount}");
        }
    }
}
