using System;

namespace Cardoni;

public class Cooldown
{
	public const uint StacksPerCharge = 1200;

	uint activateStacks;

	uint startingStacks = 0;

	public Entity Entity { get; set; }

	protected Action Action;

	public Cooldown(Entity entity, Action action)
	{
		Entity = entity;
		Action = action;
	}

	void Tick()
	{
		activateStacks += Entity.AttackSpeed;
		while (activateStacks >= StacksPerCharge)
		{
			Action();
			activateStacks -= StacksPerCharge;
		}
	}

	public void Start()
	{
		GameState.AddTicked(Tick);
		activateStacks = startingStacks;
	}

	public void End()
	{
		GameState.RemoveTicked(Tick);
	}
}
