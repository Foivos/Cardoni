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
		return source.VerticalDistance(other) <= Within;
	}
}
