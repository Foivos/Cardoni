namespace Cardoni;

using System.Collections.Generic;

public abstract class Effect
{
	public Entity Entity;

	public abstract EffectType EffectType { get; }

	public Effect RefEffect
	{
		get { return Entity.Effects[(int)EffectType]; }
		set { Entity.Effects[(int)EffectType] = value; }
	}

	public List<ICondition> Conditions { get; set; } = new();

	public virtual bool Affected()
	{
		return true;
	}

	public virtual void Update() { }

	public virtual void End()
	{
		foreach (ICondition condition in Conditions)
		{
			condition.End();
		}
		Conditions = new();
	}
}
