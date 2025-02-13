namespace Cardoni;

using Godot;

public class CountingExpiringEffectCondition<T> : ExpiringEffectCondition<T>
	where T : CountedEffect
{
	public override EffectType EffectType => EffectType.Haste;

	public uint Modifier;

	public CountingExpiringEffectCondition(Entity entity, uint duration)
		: base(entity)
	{
		if (duration != 0)
			Expiring = new Expiring(duration, End);

		Effect.Count += 1;
	}

	public override void End()
	{
		base.End();
		Effect.Count -= 1;
	}
}
