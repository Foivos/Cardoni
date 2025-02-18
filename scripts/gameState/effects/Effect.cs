namespace Cardoni;

using System;
using System.Collections.Generic;

public abstract class Effect
{
	public Entity Entity;

	int count;
	public int Count
	{
		get => count;
		set
		{
			count = Math.Max(0, value);
			Update();
		}
	}

	public abstract EffectType EffectType { get; }

	public Effect RefEffect
	{
		get { return Entity.Effects[(int)EffectType]; }
		set { Entity.Effects[(int)EffectType] = value; }
	}

	public List<Condition> Conditions { get; set; } = new();

	public static Type[] EffectTypes = new Type[]
	{
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

	public bool Active { get; set; }

	public Effect(Entity entity)
	{
		Entity = entity;
		RefEffect = this;
		Count = 0;

		Update();
	}

	public virtual void Update()
	{
		if (Active && Count == 0)
		{
			Remove();
			Active = false;
		}
		else if (!Active && Count != 0)
		{
			Apply();
			Active = true;
		}
	}

	public virtual void End()
	{
		while (Conditions.Count > 0)
		{
			Conditions[0].End();
		}
		Conditions = new();
	}

	protected abstract void Apply();

	protected abstract void Remove();
}
