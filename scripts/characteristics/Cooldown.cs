namespace Cardoni;

using System;
using Godot;

public partial class Cooldown : TickedCharacteristic<CooldownData>
{
	public uint Stacks { get; set; }

	public uint StacksPerCharge => Data.MaxStacks;

	public uint StartingStacks => Data.StartingStacks;

	public EntityActive Active => Data.Active;

	public Cooldown(Entity entity, CooldownData data)
		: base(entity, data) { }

	public override void Tick()
	{
		Stacks += Entity.AttackSpeed;
		while (Stacks >= StacksPerCharge)
		{
			Active.Activate(Entity);
			Stacks -= StacksPerCharge;
		}
	}

	public override void Start()
	{
		base.Start();
		Stacks = StartingStacks;
	}
}
