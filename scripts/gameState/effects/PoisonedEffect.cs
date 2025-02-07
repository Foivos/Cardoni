namespace Cardoni;

public class PoisonedEffect : Effect, ITicked
{
	public override EffectType EffectType => EffectType.Poisoned;

	public const uint StacksPerHealth = 1200;

	uint strength;
	public uint Strength
	{
		get { return strength; }
		set
		{
			strength = value;
			Update();
		}
	}
	public uint Stacks { get; set; }

	public bool Ticking = false;

	public PoisonedEffect(Entity entity, uint strength)
	{
		Strength = strength;
		Entity = entity;

		RefEffect = this;
		Update();
	}

	public void Tick(uint tick)
	{
		Stacks += Strength;
		if (Stacks >= StacksPerHealth)
		{
			int damage = (int)(Stacks / StacksPerHealth);
			Stacks %= StacksPerHealth;
			Entity.Damage(damage);
		}
	}

	public override void Update()
	{
		base.Update();
		if (Ticking && Strength == 0)
		{
			Ticking = false;
			GameState.Instance.RemoveTicked(this);
		}
		else if (!Ticking && Strength > 0)
		{
			Ticking = true;
			GameState.Instance.AddTicked(this);
		}
	}

	public override void End()
	{
		base.End();

		if (Ticking)
		{
			GameState.Instance.RemoveTicked(this);
		}
	}
}
