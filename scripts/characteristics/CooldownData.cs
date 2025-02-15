namespace Cardoni;

using System;
using Godot;

[GlobalClass]
public partial class CooldownData : CharacteristicData
{
	[Export]
	public uint StartingStacks { get; set; } = 0;

	[Export]
	public uint MaxStacks { get; set; } = 1200;

	[Export]
	public EntityActive Active { get; set; }

	public override Cooldown Create(Entity entity)
	{
		return new Cooldown(entity, this);
	}
}
