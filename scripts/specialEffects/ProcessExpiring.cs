namespace Cardoni;

using System;
using Godot;

public class ProcessExpiring : IComparable, IEquatable<ProcessExpiring>
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

	public ProcessExpiring(uint duration, Action onExpire)
		: this(duration, onExpire, 1) { }

	public int CompareTo(object other)
	{
		return (int)End - (int)((Expiring)other).End;
	}

	public bool Equals(ProcessExpiring other)
	{
		return this == other;
	}
}
