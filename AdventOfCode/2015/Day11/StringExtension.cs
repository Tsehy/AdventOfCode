using System.Text;

namespace AdventOfCode._2015.Day11
{
    public static class StringExtension
    {
        public static string ToValidBase(this string str)
        {
            var builder = new StringBuilder(str);

            bool hasInvalid = false;
            for (int i = 0; i < builder.Length; i++)
            {
                if (hasInvalid)
                {
                    builder[i] = 'a';
                    continue;
                }
                if ("ilo".Contains(builder[i]))
                {
                    builder[i] = (char)(builder[i] + 1);
                    hasInvalid = true;
                }
            }
            return builder.ToString();
        }

        public static bool HasSequence(this string str)
        {
            bool hasSequence = false;
            for (int i = 0; i < str.Length - 2; i++)
            {
                if (str[i] + 1 == str[i + 1] && str[i] + 2 == str[i + 2])
                {
                    hasSequence = true;
                }
            }
            return hasSequence;
        }

        public static string Increase(this string str)
        {
            var builder = new StringBuilder(str);

            int i = builder.Length - 1;

            while (builder[i] == 'z')
            {
                builder[i] = 'a';
                i--;
            }

            if ("ilo".Contains(builder[i]))
            {
                builder[i] = (char)(builder[i] + 2);
            }
            else
            {
                builder[i] = (char)(builder[i] + 1);
            }

            return builder.ToString();
        }
    }
}