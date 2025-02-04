using System;
using State;

public class Expiring : IComparable
{
    public uint End;

    public Action<uint> OnExpire;

    public uint Repeat;
    public uint Duration;

    public Expiring(uint duration, Action<uint> onExpire, uint repeat)
    {
        OnExpire = onExpire;
        End = GameState.Instance.Tick + duration;
        Repeat = repeat;
        Duration = duration;
        GameState.Instance.AddExpiring(this);
    }

    public Expiring(uint duration, Action<uint> onExpire)
        : this(duration, onExpire, 1) { }

    public int CompareTo(object other)
    {
        return (int)End - (int)((Expiring)other).End;
    }
}
