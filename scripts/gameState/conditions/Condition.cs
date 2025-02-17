using Godot;

namespace Cardoni;

public class Condition
{
	public EffectType EffectType => Data.EffectType;

	public Entity Entity { get; set; }

	public ConditionData Data { get; set; }

	public Expiring Expiring { get; set; }

	public Effect Effect
	{
		get { return Entity.Effects[(int)EffectType]; }
		set { Entity.Effects[(int)EffectType] = value; }
	}

	public Condition(Entity entity, ConditionData data)
	{
		Entity = entity;
		Data = data;
		Effect.Conditions.Add(this);
		Effect.Count += Data.Strength;
		if (data.Duration > 0)
		{
			Expiring = new Expiring(data.Duration, End);
		}
	}

	public void End()
	{
		Effect.Conditions.Remove(this);
		Effect.Count -= Data.Strength;
		if (Expiring != null)
		{
			GameState.RemoveExpiring(Expiring);
		}
	}
}
