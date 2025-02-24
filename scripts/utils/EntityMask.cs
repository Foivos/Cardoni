namespace Cardoni;

using System;
using System.Collections;
using System.Collections.Generic;
using Godot;

[Flags]
public enum EntityMasks
{
	Enemy = 1 << 0,
	Friendly = 1 << 1,
	Mobile = 1 << 2,
	Structure = 1 << 3,
	Boss = 1 << 4,
}

[GlobalClass]
public partial class EntityMask : Resource, IEnumerable<EntityMasks>
{
	public uint Mask { get; set; }

	[Export]
	public EntityMasks Masks
	{
		get => (EntityMasks)Mask;
		set { Mask = (uint)value; }
	}

	public EntityMask() { }

	public bool Matches(EntityMask other)
	{
		return (Mask & other.Mask) == Mask;
	}

	public void Add(EntityMasks mask)
	{
		Mask |= (uint)mask;
	}

	public void Remove(EntityMasks mask)
	{
		Mask &= ~(uint)mask;
	}

	public IEnumerator<EntityMasks> GetEnumerator()
	{
		List<EntityMasks> list = new();
		foreach (EntityMasks mask in Enum.GetValues(typeof(EntityMasks)))
		{
			if (((uint)mask & Mask) != 0)
			{
				list.Add(mask);
			}
		}
		return list.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}
