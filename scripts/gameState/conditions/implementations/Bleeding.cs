namespace Cardoni;

using Godot;

public class Bleeding : ExpiringEffectCondition<PoisonedEffect>
{
	public override EffectType EffectType => EffectType.Bleeding;

	public uint Strength;

	public Bleeding(Entity entity, uint strength, uint duration)
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
