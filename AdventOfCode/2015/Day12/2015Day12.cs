using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class _2015Day12 : _2015Day
    {
        public _2015Day12() : base("Day12")
        {
        }

        public override void Part1()
        {
            base.Part1();

            int sum = Regex.Matches(Input[0], @"-?\d+").Sum(m => int.Parse(m.Value));

            Console.WriteLine($"Sum of the numbers: {sum}");
        }

        public override void Part2()
        {
            base.Part2();

            using var reader = new JsonTextReader(new StringReader(Input[0]));
            int sum = CountObjectValue(reader);

            Console.WriteLine($"Sum of the numbers without red: {sum}");
        }

        private static int CountObjectValue(JsonTextReader reader, JsonToken? type = null)
        {
            int value = 0;
            bool redObject = false;

            while (reader.Read())
            {
                switch (reader.TokenType)
                {
                    case JsonToken.StartObject:
                    case JsonToken.StartArray:
                        value += CountObjectValue(reader, reader.TokenType); // start new counting
                        continue;

                    case JsonToken.EndObject:
                    case JsonToken.EndArray:
                        return redObject ? 0 : value; // end the counting

                    case JsonToken.Integer:
                        value += int.Parse(reader.Value?.ToString()!); // parse number
                        continue;

                    case JsonToken.String:
                        if (type == JsonToken.StartObject && reader.Value?.ToString() == "red") // red object
                        {
                            redObject = true;
                        }
                        continue;
                }
            }

            return value;
        }
    }
}
