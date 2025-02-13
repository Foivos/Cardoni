namespace Cardoni;

using System;
using Godot;
using Godot.Collections;

[GlobalClass]
public partial class CardRegistry : Resource
{
	[Export]
	public Dictionary<string, CardResource> Cards { get; set; } = new();

	public enum YourEnum
	{
		One,
		Two,
		Three,
		Four,
	}

	public uint Mask { get; set; }

	[Export]
	public Array<YourEnum> Masks
	{
		get
		{
			System.Collections.Generic.List<YourEnum> list = new();
			foreach (YourEnum value in Enum.GetValues<YourEnum>())
			{
				if ((Mask & 1 << (int)value) != 0)
				{
					list.Add(value);
				}
			}
			return new(list);
		}
		set
		{
			//Mask = 0;
			foreach (EntityMasks mask in value)
			{
				Mask |= (uint)mask;
			}
		}
	}
}
