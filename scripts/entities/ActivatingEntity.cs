using System;
using Godot;

namespace Cardoni;

public abstract partial class ActivatingEntity : Entity
{
	public const uint StacksPerCharge = 1200;

	uint activateStacks;

	uint startingStacks = 0;

	void Tick()
	{
		activateStacks += AttackSpeed;
		while (activateStacks >= StacksPerCharge)
		{
			Activate();
			activateStacks -= StacksPerCharge;
		}
	}

	protected abstract void Activate();

	public override void Spawn()
	{
		base.Spawn();
		GameState.AddTicked(Tick);
		activateStacks = startingStacks;
	}

	public override void Kill()
	{
		base.Kill();
		GameState.RemoveTicked(Tick);
	}
}
