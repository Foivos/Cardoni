namespace Cardoni;

using Godot;

public abstract class ExpiringEffectCondition<T> : EffectCondition<T>
	where T : Effect
{
	public Expiring Expiring { get; set; }

	public override void End()
	{
		base.End();
		if (Expiring != null)
		{
			GameState.RemoveExpiring(Expiring);
		}
	}
}
