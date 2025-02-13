namespace Cardoni;

public class PoisonedEffect : Effect
{
	public const EffectType Type = EffectType.Poisoned;
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

	public PoisonedEffect(Entity entity)
		: base(entity)
	{
		Strength = 0;
	}

	public override bool Affected()
	{
		return Stacks > 0;
	}

	public void Tick()
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
			GameState.RemoveTicked(Tick);
		}
		else if (!Ticking && Strength > 0)
		{
			Ticking = true;
			GameState.AddTicked(Tick);
		}
	}
}
