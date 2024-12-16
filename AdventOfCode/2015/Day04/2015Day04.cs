using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class _2015Day04 : _2015Day
    {
        private readonly string secretKey;

        public _2015Day04() : base("Day04")
        {
            secretKey = Input[0];
        }

        public override void Part1()
        {
            base.Part1();

            int lowest = GetLowestNumber(secretKey, "^00000");

            Console.WriteLine($"Lowest number: {lowest}\n");
        }

        public override void Part2()
        {
            base.Part2();

            int lowest = GetLowestNumber(secretKey, "^000000");

            Console.WriteLine($"Lowest number: {lowest}\n");
        }

        #region Private methods
        private static int GetLowestNumber(string input, string pattern)
        {
            int i = 0;
            var regexPattern = new Regex(pattern);

            using System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            while (true)
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes($"{input}{i}");
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                string a = Convert.ToHexString(hashBytes);
                if (regexPattern.IsMatch(a))
                {
                    return i;
                }

                i++;
            }
        }
        #endregion
    }
}
