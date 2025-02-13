namespace Cardoni;

using System;
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

	public static Type[] EffectTypes = new Type[]{
		typeof(WetEffect),
		typeof(FrozenEffect),
		typeof(StunnedEffect),
		typeof(PoisonedEffect),
		typeof(ElectrifiedEffect),
		typeof(BleedingEffect),
		typeof(SlowedEffect),
		typeof(HasteEffect),
		typeof(RestrictedEffect),
		typeof(ConfusedEffect),
		typeof(MindControlledEffect),
	};

	public Effect(Entity entity) {
		Entity = entity;
		RefEffect = this;

		Update();

	}

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
