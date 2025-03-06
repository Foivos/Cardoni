namespace Cardoni;

using System;

public class Expiring : IComparable<Expiring>, IEquatable<Expiring>
{
	public uint End;

	public Action OnExpire;

	public uint Repeat;
	public uint Duration;

	public Expiring(uint duration, Action onExpire, uint repeat)
	{
		OnExpire = onExpire;
		End = GameState.Tick + duration;
		Repeat = repeat;
		Duration = duration;
		GameState.AddExpiring(this);
	}

	public Expiring(uint duration, Action onExpire)
		: this(duration, onExpire, 1) { }

	public int CompareTo(Expiring other)
	{
		return End.CompareTo(other.End);
	}

	public bool Equals(Expiring other)
	{
		return this == other;
	}
}
