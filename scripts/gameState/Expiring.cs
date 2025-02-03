using System;

public class Expiring : IComparable
{
    public uint End;

    public Action<uint> OnExpire;

    private Expiring(uint duration, Action<uint> onExpire) {
        OnExpire = onExpire;
        End = GameState.Instance.Tick + duration;
    }

    public static void Create(uint duration, Action<uint> onExpire) {
        Expiring expiring = new Expiring(duration, onExpire);
        GameState.Instance.AddExpiring(expiring);
    }

    public static void CreateRecurring(uint duration, Action<uint> action) {
        Action<uint> onExpire;
        onExpire = (uint tick) => {
            action(tick);
            Create(duration, onExpire);
        };
        Create(duration, onExpire);
    }

    public int CompareTo(object other) {
        return (int)End - (int)((Expiring) other).End;
    }
}
