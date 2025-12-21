using AdventOfCode._2015.Day07;

namespace AdventOfCode
{
    public class _2015Day07 : _2015Day
    {
        private readonly GateFactory factory;

        public _2015Day07() : base("Day07")
        {
            factory = new GateFactory();
            ImportData();
        }

        public override void Part1()
        {
            base.Part1();

            Console.WriteLine($"Signal on wire 'a': {factory.CreatedGates["a"].Value}");
        }

        public override void Part2()
        {
            base.Part2();

            if (factory.CreatedGates["b"] is SignalGate gateB)
            {
                gateB.Value = factory.CreatedGates["a"].Value;
                factory.CreatedGates["a"].RecalculateValue();
                Console.WriteLine($"New signal on wire 'a': {factory.CreatedGates["a"].Value}");
            }
            else
            {
                Console.WriteLine("B is not a signal!");
            }
        }

        #region Private methods
        private void ImportData()
        {
            List<string> GateList = [.. Input];

            while (GateList.Count > 0)
            {
                for (int i = GateList.Count - 1; i >= 0; i--)
                {
                    bool success = factory.TryProcessGateData(GateList[i]);
                    if (success)
                    {
                        GateList.RemoveAt(i);
                    }
                }
            }
        }
        #endregion
    }
}
