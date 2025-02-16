namespace Cardoni;

using System;
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
public partial class EntityMask : Resource
{
	public uint Mask { get; set; }

	[Export]
	public EntityMasks Masks
	{
		get => (EntityMasks)Mask;
		set { Mask = (uint)value; }
	}

	public EntityMask() { }

	public EntityMask(EntityMasks[] masks)
	{
		Mask = 0;
		foreach (EntityMasks mask in masks)
		{
			Mask |= (uint)mask;
		}
	}

	public bool Matches(EntityMask other)
	{
		return (Mask & other.Mask) == Mask;
	}
}
