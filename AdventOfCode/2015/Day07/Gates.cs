namespace AdventOfCode._2015.Day07
{
    public interface IGate
    {
        public int Value { get; }
        public bool Recalculated { get; }

        public void RecalculateValue();
    }

    #region Signal
    public class SignalGate(int value) : IGate
    {
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
        public int Value { get; private set; }
        public bool Recalculated { get; private set; }
        private readonly IGate _input;

        public WireGate(IGate input)
        {
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
        public int Value { get; private set; }
        public bool Recalculated { get; private set; }
        private readonly IGate _left;
        private readonly IGate _right;

        public AndGate(IGate left, IGate right)
        {
            _left = left;
            _right = right;
            Value = _left.Value & _right.Value;
            Recalculated = false;
        }

        public AndGate(int leftNum, IGate right)
        {
            _left = new SignalGate(leftNum);
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
        public int Value { get; private set; }
        public bool Recalculated { get; private set; }
        private readonly IGate _left;
        private readonly IGate _right;

        public OrGate(IGate left, IGate right)
        {
            _left = left;
            _right = right;
            Value = _left.Value | _right.Value;
            Recalculated = false;
        }

        public OrGate(int leftNum, IGate right)
        {
            _left = new SignalGate(leftNum);
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
        public int Value { get; private set; }
        public bool Recalculated { get; private set; }
        private readonly IGate _input;

        public NotGate(IGate input)
        {
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
        public int Value { get; private set; }
        public bool Recalculated { get; private set; }
        private readonly IGate _input;
        private readonly int _num;

        public LShiftGate(IGate input, int num)
        {
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

    #region RShift
    public class RShiftGate : IGate
    {
        public int Value { get; private set; }
        public bool Recalculated { get; private set; }
        private readonly IGate _input;
        private readonly int _num;

        public RShiftGate(IGate input, int num)
        {
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
