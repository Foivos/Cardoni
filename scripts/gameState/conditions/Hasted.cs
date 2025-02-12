namespace Cardoni;

using Godot;

public class Hasted : ExpiringEffectCondition<HasteEffect>
{
	public override EffectType EffectType => EffectType.Hasted;

	public uint Modifier;

	public Hasted(Entity entity, uint duration)
	{
		if (duration != 0)
			Expiring = new Expiring(duration, OnExpire);

		Entity = entity;

		Effect ??= new HasteEffect(Entity);

		Effect.Count += 1;
	}

	public void OnExpire()
	{
		End();
		Effect.Count -= 1;
	}

	public override void End()
	{
		base.End();
		Effect.Count -= 1;
	}
}
