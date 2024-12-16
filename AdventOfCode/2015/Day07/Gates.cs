namespace AdventOfCode._2015.Day07
{
    public interface IGate
    {
        public string Name { get; init; }
        public int Value { get; }
        public bool Recalculated { get; }

        public void RecalculateValue();
    }

    #region Signal
    public class SignalGate(string name, int value) : IGate
    {
        public string Name { get; init; } = name;
        public int Value { get; set; } = value;
        public bool Recalculated { get; private set; } = false;

        public void RecalculateValue()
        {
            Recalculated = true;
        }
    }
    #endregion

    #region Wire
    public class WireGate : IGate
    {
        public string Name { get; init; }
        public int Value { get; private set; }
        public bool Recalculated { get; private set; }
        private readonly IGate _input;

        public WireGate(string name, IGate input)
        {
            Name = name;
            _input = input;
            Value = _input.Value;
            Recalculated = false;
        }

        public void RecalculateValue()
        {
            if (!_input.Recalculated)
            {
                _input.RecalculateValue();
            }
            Value = _input.Value;
            Recalculated = true;
        }
    }
    #endregion

    #region And
    public class AndGate : IGate
    {
        public string Name { get; init; }
        public int Value { get; private set; }
        public bool Recalculated { get; private set; }
        private readonly IGate _left;
        private readonly IGate _right;

        public AndGate(string name, IGate left, IGate right)
        {
            Name = name;
            _left = left;
            _right = right;
            Value = _left.Value & _right.Value;
            Recalculated = false;
        }

        public AndGate(string name, int leftNum, IGate right)
        {
            Name = name;
            _left = new SignalGate(name + leftNum, leftNum);
            _right = right;
            Value = _left.Value & _right.Value;
            Recalculated = false;
        }

        public void RecalculateValue()
        {
            if (!_left.Recalculated)
            {
                _left.RecalculateValue();
            }
            if (!_right.Recalculated)
            {
                _right.RecalculateValue();
            }
            Value = _left.Value & _right.Value;
            Recalculated = true;
        }
    }
    #endregion

    #region Or
    public class OrGate : IGate
    {
        public string Name { get; init; }
        public int Value { get; private set; }
        public bool Recalculated { get; private set; }
        private readonly IGate _left;
        private readonly IGate _right;

        public OrGate(string name, IGate left, IGate right)
        {
            Name = name;
            _left = left;
            _right = right;
            Value = _left.Value | _right.Value;
            Recalculated = false;
        }

        public OrGate(string name, int leftNum, IGate right)
        {
            Name = name;
            _left = new SignalGate(name + leftNum, leftNum);
            _right = right;
            Value = _left.Value | _right.Value;
            Recalculated = false;
        }

        public void RecalculateValue()
        {
            if (!_left.Recalculated)
            {
                _left.RecalculateValue();
            }
            if (!_right.Recalculated)
            {
                _right.RecalculateValue();
            }
            Value = _left.Value | _right.Value;
            Recalculated = true;
        }
    }
    #endregion

    #region Not
    public class NotGate : IGate
    {
        public string Name { get; init; }
        public int Value { get; private set; }
        public bool Recalculated { get; private set; }
        private readonly IGate _input;

        public NotGate(string name, IGate input)
        {
            Name = name;
            _input = input;
            Value = 65535 - _input.Value; // 16bit bitwise complement
            Recalculated = false;
        }

        public void RecalculateValue()
        {
            if (!_input.Recalculated)
            {
                _input.RecalculateValue();
            }
            Value = 65535 - _input.Value;
            Recalculated = true;
        }
    }
    #endregion

    #region LShift
    public class LShiftGate : IGate
    {
        public string Name { get; init; }
        public int Value { get; private set; }
        public bool Recalculated { get; private set; }
        private readonly IGate _input;
        private readonly int _num;

        public LShiftGate(string name, IGate input, int num)
        {
            Name = name;
            _input = input;
            _num = num;
            Value = _input.Value << _num;
            Recalculated = false;
        }

        public void RecalculateValue()
        {
            if (!_input.Recalculated)
            {
                _input.RecalculateValue();
            }
            Value = _input.Value << _num;
            Recalculated = true;
        }
    }
    #endregion

    #region RShidt
    public class RShiftGate : IGate
    {
        public string Name { get; init; }
        public int Value { get; private set; }
        public bool Recalculated { get; private set; }
        private readonly IGate _input;
        private readonly int _num;

        public RShiftGate(string name, IGate input, int num)
        {
            Name = name;
            _input = input;
            _num = num;
            Value = _input.Value >> _num;
            Recalculated = false;
        }

        public void RecalculateValue()
        {
            if (!_input.Recalculated)
            {
                _input.RecalculateValue();
            }
            Value = _input.Value >> _num;
            Recalculated = true;
        }
    }
    #endregion
}
