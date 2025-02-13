namespace Cardoni;

using Godot;

public class Poisoned : ExpiringEffectCondition<PoisonedEffect>
{
	public override EffectType EffectType => EffectType.Poisoned;

	public uint Strength;

	public Poisoned(Entity entity, uint strength, uint duration)
		: base(entity)
	{
		if (duration != 0)
			Expiring = new Expiring(duration, End);

		Entity = entity;
		Strength = strength;

		Effect.Strength += Strength;
	}

	public override void End()
	{
		base.End();
		Effect.Strength -= Strength;
	}
}
