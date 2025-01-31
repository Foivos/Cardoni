
using System;

public abstract class Condition: IComparable {
    public uint Ends;

    public int CompareTo(object other) {
        return (int)Ends - (int)((Condition) other).Ends;
    }

    public abstract void End();
}
