namespace Cardoni;

using System;

public class ProcessExpiring : IComparable<ProcessExpiring>, IEquatable<ProcessExpiring>
{
	public float End;

	public Action OnExpire;

	public uint Repeat;
	public float Duration;

	public ProcessExpiring(float duration, Action onExpire, uint repeat)
	{
		OnExpire = onExpire;
		End = SpecialState.CurrentTime + duration;
		Repeat = repeat;
		Duration = duration;
		SpecialState.AddExpiring(this);
	}

	public ProcessExpiring(float duration, Action onExpire)
		: this(duration, onExpire, 1) { }

	public int CompareTo(ProcessExpiring other)
	{
		return End.CompareTo(other.End);
	}

	public bool Equals(ProcessExpiring other)
	{
		return this == other;
	}
}
