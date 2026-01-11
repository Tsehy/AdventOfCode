namespace AdventOfCode._2018.Day16;

internal enum OpCodes : ushort
{
    addr = 0b0000000000000001,
    addi = 0b0000000000000010,
    mulr = 0b0000000000000100,
    muli = 0b0000000000001000,
    banr = 0b0000000000010000,
    bani = 0b0000000000100000,
    borr = 0b0000000001000000,
    bori = 0b0000000010000000,
    setr = 0b0000000100000000,
    seti = 0b0000001000000000,
    gtir = 0b0000010000000000,
    gtri = 0b0000100000000000,
    gtrr = 0b0001000000000000,
    eqir = 0b0010000000000000,
    eqri = 0b0100000000000000,
    eqrr = 0b1000000000000000,
}

internal struct Command(int opcode, int param1, int param2, int output)
{
    public int OpCode { get; set; } = opcode;
    public int Param1 { get; set; } = param1;
    public int Param2 { get; set; } = param2;
    public int Outout { get; set; } = output;

    public Command(int[] param) : this(param[0], param[1], param[2], param[3]) { }
}

internal class Observation(int[] before, Command cmd, int[] after)
{
    public int[] Before { get; set; } = before;
    public Command Command { get; set; } = cmd;
    public int[] After { get; set; } = after;
}

internal class Interpreter
{
    private readonly Dictionary<int, OpCodes> OpCodeMapping = [];

    public Interpreter(List<Observation> obs)
    {
        var dict = Enumerable.Range(0, 16).ToDictionary(v => v, v => ushort.MaxValue);

        foreach (var ob in obs)
            dict[ob.Command.OpCode] &= TestBitMask(ob);

        while (dict.Count > 0)
        {
            int key = 0;
            ushort bitmask = 0;
            foreach ((int dKey, ushort dBitmask) in dict)
                if (BitCount(dBitmask) == 1)
                    (key, bitmask) = (dKey, dBitmask);

            OpCodeMapping[key] = (OpCodes)bitmask;
            dict.Remove(key);

            ushort negate = (ushort)~bitmask;
            foreach (int otherKey in dict.Keys)
                dict[otherKey] &= negate;
        }
    }

    private static ushort TestBitMask(Observation ob)
    {
        ushort bitmask = 0;
        foreach (var op in Enum.GetValues(typeof(OpCodes)).Cast<OpCodes>())
            if (ob.After.SequenceEqual(Run(ob.Before, ob.Command, op)))
                bitmask |= (ushort)op;

        return bitmask;
    }

    private static int BitCount(ushort bitmask)
    {
        int count;
        for (count = 0; bitmask != 0; bitmask >>= 1)
            count += bitmask & 1;

        return count;
    }

    public static int Test(Observation ob) => BitCount(TestBitMask(ob));

    public int[] Run(List<Command> cmds)
    {
        int[] reg = new int[4];

        foreach (var cmd in cmds)
        {
            var op = OpCodeMapping[cmd.OpCode];
            reg = Run(reg, cmd, op);
        }

        return reg;
    }

    private static int[] Run(int[] initialState, Command cmd, OpCodes op)
    {
        int[] res = new int[4];
        initialState.CopyTo(res, 0);

        switch (op)
        {
            case OpCodes.addr:
                if (cmd.Param1 < 4 && cmd.Param2 < 4)
                    res[cmd.Outout] = res[cmd.Param1] + res[cmd.Param2];
                break;

            case OpCodes.addi:
                if (cmd.Param1 < 4)
                    res[cmd.Outout] = res[cmd.Param1] + cmd.Param2;
                break;

            case OpCodes.mulr:
                if (cmd.Param1 < 4 && cmd.Param2 < 4)
                    res[cmd.Outout] = res[cmd.Param1] * res[cmd.Param2];
                break;

            case OpCodes.muli:
                if (cmd.Param1 < 4)
                    res[cmd.Outout] = res[cmd.Param1] * cmd.Param2;
                break;

            case OpCodes.banr:
                if (cmd.Param1 < 4 && cmd.Param2 < 4)
                    res[cmd.Outout] = res[cmd.Param1] & res[cmd.Param2];
                break;

            case OpCodes.bani:
                if (cmd.Param1 < 4)
                    res[cmd.Outout] = res[cmd.Param1] & cmd.Param2;
                break;

            case OpCodes.borr:
                if (cmd.Param1 < 4 && cmd.Param2 < 4)
                    res[cmd.Outout] = res[cmd.Param1] | res[cmd.Param2];
                break;

            case OpCodes.bori:
                if (cmd.Param1 < 4)
                    res[cmd.Outout] = res[cmd.Param1] | cmd.Param2;
                break;

            case OpCodes.setr:
                if (cmd.Param1 < 4)
                    res[cmd.Outout] = res[cmd.Param1];
                break;

            case OpCodes.seti:
                if (cmd.Param1 < 4)
                    res[cmd.Outout] = cmd.Param1;
                break;

            case OpCodes.gtir:
                if (cmd.Param2 < 4)
                    res[cmd.Outout] = cmd.Param1 > res[cmd.Param2] ? 1 : 0;
                break;

            case OpCodes.gtri:
                if (cmd.Param1 < 4)
                    res[cmd.Outout] = res[cmd.Param1] > cmd.Param2 ? 1 : 0;
                break;

            case OpCodes.gtrr:
                if (cmd.Param1 < 4 && cmd.Param2 < 4)
                    res[cmd.Outout] = res[cmd.Param1] > res[cmd.Param2] ? 1 : 0;
                break;

            case OpCodes.eqir:
                if (cmd.Param2 < 4)
                    res[cmd.Outout] = cmd.Param1 == res[cmd.Param2] ? 1 : 0;
                break;

            case OpCodes.eqri:
                if (cmd.Param1 < 4)
                    res[cmd.Outout] = res[cmd.Param1] == cmd.Param2 ? 1 : 0;
                break;

            case OpCodes.eqrr:
                if (cmd.Param1 < 4 && cmd.Param2 < 4)
                    res[cmd.Outout] = res[cmd.Param1] == res[cmd.Param2] ? 1 : 0;
                break;
        }

        return res;
    }
}