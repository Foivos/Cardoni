namespace Cardoni;

using Godot;

public class Slowed : ExpiringEffectCondition<SlowedEffect>
{
	public override EffectType EffectType => EffectType.Slowed;

	private Slowed(Entity entity, uint modifier, uint duration)
	{
		Expiring = new Expiring(duration, OnExpire);

		Entity = entity;

		if (Effect == null)
		{
			Effect = new SlowedEffect(Entity);
		}
		else
		{
			Effect.Count += 1;
		}
	}

	public void OnExpire()
	{
		Effect.Count -= 1;
		Effect.Update();
	}
}
