namespace Cardoni;

using System;
using Godot;

[GlobalClass]
public partial class VerticalDistanceFilter : EntityFilter
{
	[Export]
	public int Within { get; set; }

	public override bool IsValid(Entity source, Entity other)
	{
		return Math.Abs(source.Y - other.Y) - source.Height - other.Height <= Within;
	}
}
