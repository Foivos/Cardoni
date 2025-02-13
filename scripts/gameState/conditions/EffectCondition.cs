namespace Cardoni;

public abstract class EffectCondition<T> : ICondition
	where T : Effect
{
	public abstract EffectType EffectType { get; }

	public Entity Entity { get; set; }

	public T Effect
	{
		get { return (T)Entity.Effects[(int)EffectType]; }
		set { Entity.Effects[(int)EffectType] = value; }
	}

	public EffectCondition(Entity entity)
	{
		Entity = entity;
	}

	public virtual void End() { }
}
