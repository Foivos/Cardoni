namespace Cardoni;

using System;

public class Expiring : IComparable, IEquatable<Expiring>
{
	public uint End;

	public Action OnExpire;

	public uint Repeat;
	public uint Duration;

	public Expiring(uint duration, Action onExpire, uint repeat)
	{
		OnExpire = onExpire;
		End = GameState.Instance.Tick + duration;
		Repeat = repeat;
		Duration = duration;
		GameState.Instance.AddExpiring(this);
	}

	public Expiring(uint duration, Action onExpire)
		: this(duration, onExpire, 1) { }

	public int CompareTo(object other)
	{
		return (int)End - (int)((Expiring)other).End;
	}

	public bool Equals(Expiring other)
	{
		return this == other;
	}
}
