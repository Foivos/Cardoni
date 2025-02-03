using System;

class Recurring {
    Action<uint> Action;
    uint Duration;

    public Recurring(uint duration, Action<uint> action) {
        Action = action;
        Duration = duration;
    }

    public void OnExpire(uint tick) {
        Action(tick);
        Expiring.Create(Duration, OnExpire);
    }
}

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
        Recurring recurring = new Recurring(duration, action);
        Create(duration, recurring.OnExpire);
    }

    public int CompareTo(object other) {
        return (int)End - (int)((Expiring) other).End;
    }
}
