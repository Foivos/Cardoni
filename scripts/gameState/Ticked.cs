namespace Cardoni;

using System;

public class Ticked : ITicked
{
    public Action<uint> OnTick;

    public Ticked(Action<uint> onTick)
    {
        OnTick = onTick;
        GameState.Instance.AddTicked(this);
    }

    public void Tick(uint tick)
    {
        OnTick(tick);
    }
}
