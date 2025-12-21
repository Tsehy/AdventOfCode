namespace AdventOfCode
{
    public class _2021Day18 : _2021Day
    {
        public _2021Day18() : base("Day18")
        {

        }

        public override void Part1()
        {
            base.Part1();

            Node sumAll = Convert(Input[0]);

            for (int i = 1; i < Input.Length; i++)
            {
                sumAll = new Node(sumAll, Convert(Input[i]), null);
                sumAll.Simplify();
            }

            Console.WriteLine($"The magnitude of the final sum: {sumAll.Magnitude()}");
        }

        public override void Part2()
        {
            base.Part2();

            int maxMagnitude = 0;

            for (int i = 0; i < Input.Length; i++)
            {
                for (int j = i + 1; j < Input.Length; j++)
                {
                    maxMagnitude = GetMaxMagnitude(Input[i], Input[j], maxMagnitude);
                    maxMagnitude = GetMaxMagnitude(Input[j], Input[i], maxMagnitude);
                }
            }

            Console.WriteLine($"The largest magnitude of two numbers: {maxMagnitude}");
        }

        #region Private methods
        private static Node Convert(string Input)
        {
            string trimInput = Input.Substring(1, Input.Length - 2);
            int middle = MiddleCommaIndex(trimInput);

            string left = trimInput.Substring(0, middle);
            string right = trimInput.Substring(middle + 1, trimInput.Length - middle - 1);

            Node leftNode;
            if (left.Contains('['))
            {
                leftNode = Convert(left);
            }
            else
            {
                leftNode = new Node(int.Parse(left), null);
            }

            Node rightNode;
            if (right.Contains('['))
            {
                rightNode = Convert(right);
            }
            else
            {
                rightNode = new Node(int.Parse(right), null);
            }

            return new Node(leftNode, rightNode, null);
        }

        private static int MiddleCommaIndex(string Input)
        {
            int depth = 0;

            for (int i = 0; i < Input.Length; i++)
            {
                switch (Input[i])
                {
                    case '[':
                        depth++;
                        break;
                    case ']':
                        depth--;
                        break;
                    case ',':
                        if (depth == 0)
                        {
                            return i;
                        }
                        break;
                }
            }

            return -1;
        }

        private static int GetMaxMagnitude(string a, string b, int currentMax)
        {
            Node sum = new Node(Convert(a), Convert(b), null);
            sum.Simplify();
            int m = sum.Magnitude();
            if (m > currentMax)
            {
                return m;
            }

            return currentMax;
        }
        #endregion
    }
}