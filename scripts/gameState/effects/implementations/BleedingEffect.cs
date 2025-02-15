namespace Cardoni;

public class BleedingEffect : Effect
{
	public const EffectType Type = EffectType.Bleeding;
	public override EffectType EffectType => EffectType.Bleeding;

	public const uint StacksPerHealth = 1200;

	public uint Stacks { get; set; }

	public bool Ticking = false;

	public BleedingEffect(Entity entity)
		: base(entity) { }

	public void Tick()
	{
		Stacks += (uint)(30 * Count);
		if (Stacks >= StacksPerHealth)
		{
			int damage = (int)(Stacks / StacksPerHealth);
			Stacks %= StacksPerHealth;
			Entity.Damage(damage);
			Count -= damage;
		}
	}

	protected override void Apply()
	{
		GameState.AddTicked(Tick);
	}

	protected override void Remove()
	{
		GameState.RemoveTicked(Tick);
	}
}
