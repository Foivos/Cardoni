using System;

public class Expiring : IComparable
{
    public uint End;

    public Action OnExpire;

    private Expiring(uint duration, Action onExpire) {
        OnExpire = onExpire;
        End = GameState.Instance.Tick + duration;
    }

    public static void Create(uint duration, Action onExpire) {
        Expiring expiring = new Expiring(duration, onExpire);
        GameState.Instance.AddExpiring(expiring);
    }

    public static void CreateRecurring(uint duration, Action action) {
        Action onExpire;
        onExpire = () => {
            action();
            Create(duration, onExpire);
        };
        Create(duration, onExpire);
    }

    public int CompareTo(object other) {
        return (int)End - (int)((Expiring) other).End;
    }
}
