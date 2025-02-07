namespace Cardoni;

using Godot;

public class Slowed : ExpiringEffectCondition<SlowedEffect>
{
	public override EffectType EffectType => EffectType.Slowed;

	public uint Modifier;

	private Slowed(Entity entity, uint modifier, uint duration)
	{
		Expiring = new Expiring(duration, OnExpire);

		Entity = entity;
		Modifier = modifier;

		if (Effect == null)
		{
			Effect = new SlowedEffect(Entity, Modifier);
		}
		else
		{
			Effect.Modifier += Modifier;
		}
	}

	public void OnExpire(uint tick)
	{
		Effect.Modifier -= Modifier;
		Effect.Update();
	}
}
