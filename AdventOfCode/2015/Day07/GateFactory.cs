using System.Text.RegularExpressions;

namespace AdventOfCode._2015.Day07
{
    public class GateFactory
    {
        private static readonly Regex _signalRegex = new(@"^(\d+) -> ([a-z]+)$");
        private static readonly Regex _wireRegex = new(@"^([a-z]+) -> ([a-z]+)$");
        private static readonly Regex _andRegex = new(@"^([a-z0-9]+) AND ([a-z]+) -> ([a-z]+)$");
        private static readonly Regex _orRegex = new(@"^([a-z0-9]+) OR ([a-z]+) -> ([a-z]+)$");
        private static readonly Regex _notRegex = new(@"^NOT ([a-z]+) -> ([a-z]+)$");
        private static readonly Regex _lShiftRegex = new(@"^([a-z]+) LSHIFT (\d+) -> ([a-z]+)$");
        private static readonly Regex _rShiftRegex = new(@"^([a-z]+) RSHIFT (\d+) -> ([a-z]+)$");

        public Dictionary<string, IGate> CreatedGates { get; set; }

        public GateFactory()
        {
            CreatedGates = [];
        }

        public bool TryProcessGateData(string str)
        {
            #region Signal
            if (_signalRegex.IsMatch(str))
            {
                Match match = _signalRegex.Match(str);
                CreatedGates.Add(
                    match.Groups[2].Value,
                    new SignalGate(
                        int.Parse(match.Groups[1].Value)
                    )
                );
                return true;
            }
            #endregion

            #region Wire
            if (_wireRegex.IsMatch(str))
            {
                Match match = _wireRegex.Match(str);
                if (CreatedGates.TryGetValue(match.Groups[1].Value, out IGate? input))
                {
                    CreatedGates.Add(
                        match.Groups[2].Value,
                        new WireGate(
                            input
                        )
                    );
                    return true;
                }
            }
            #endregion

            #region And
            if (_andRegex.IsMatch(str))
            {
                Match match = _andRegex.Match(str);
                string name = match.Groups[3].Value;
                if (CreatedGates.TryGetValue(match.Groups[2].Value, out IGate? rightGate) && CreatedGates.TryGetValue(match.Groups[1].Value, out IGate? gateLeft))
                {
                    CreatedGates.Add(
                        name,
                        new AndGate(
                            gateLeft,
                            rightGate
                        )
                    );
                    return true;
                }
                else if (int.TryParse(match.Groups[1].Value, out int leftNum) && rightGate != null)
                {
                    CreatedGates.Add(
                        name,
                        new AndGate(
                            leftNum,
                            rightGate
                        )
                    );
                    return true;
                }
            }
            #endregion

            #region Or
            if (_orRegex.IsMatch(str))
            {
                Match match = _orRegex.Match(str);
                string name = match.Groups[3].Value;
                if (CreatedGates.TryGetValue(match.Groups[2].Value, out IGate? rightGate) && CreatedGates.TryGetValue(match.Groups[1].Value, out IGate? gateLeft))
                {
                    CreatedGates.Add(
                        name,
                        new OrGate(
                            gateLeft,
                            rightGate
                        )
                    );
                    return true;
                }
                else if (int.TryParse(match.Groups[1].Value, out int leftNum) && rightGate != null)
                {
                    CreatedGates.Add(
                        name,
                        new OrGate(
                            leftNum,
                            rightGate
                        )
                    );
                    return true;
                }
            }
            #endregion

            #region Not
            if (_notRegex.IsMatch(str))
            {
                Match match = _notRegex.Match(str);
                if (CreatedGates.TryGetValue(match.Groups[1].Value, out IGate? inputGate))
                {
                    CreatedGates.Add(
                        match.Groups[2].Value,
                        new NotGate(
                            inputGate
                        )
                    );
                    return true;
                }
            }
            #endregion

            #region LShift
            if (_lShiftRegex.IsMatch(str))
            {
                Match match = _lShiftRegex.Match(str);
                if (CreatedGates.TryGetValue(match.Groups[1].Value, out IGate? inputGate))
                {
                    CreatedGates.Add(
                        match.Groups[3].Value,
                        new LShiftGate(
                            inputGate,
                            int.Parse(match.Groups[2].Value)
                        )
                    );
                    return true;
                }
            }
            #endregion

            #region RShift
            if (_rShiftRegex.IsMatch(str))
            {
                Match match = _rShiftRegex.Match(str);
                if (CreatedGates.TryGetValue(match.Groups[1].Value, out IGate? inputGate))
                {
                    CreatedGates.Add(
                        match.Groups[3].Value,
                        new RShiftGate(
                            inputGate,
                            int.Parse(match.Groups[2].Value)
                        )
                    );
                    return true;
                }
            }
            #endregion

            return false;
        }
    }
}
