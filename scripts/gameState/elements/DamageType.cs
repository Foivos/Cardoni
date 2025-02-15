namespace Cardoni;

using System;
using Godot;

public enum DamageTypes
{
	Physical,
	Fire,
	Frost,
	Thunder,
	Earth,
}

public class DamageType
{
	static Action<Entity, int>[] damageActions = new Action<Entity, int>[]
	{
		DealPhysical,
		DealFire,
		DealFrost,
		DealThunder,
		DealEarth,
	};
	static Func<Entity, int, int>[] calculateFunctions = new Func<Entity, int, int>[]
	{
		CalculatePhysical,
		CalculateFire,
		CalculateFrost,
		CalculateThunder,
		CalculateEarth,
	};

	public static void DealDamage(Entity entity, DamageTypes damageType, int damage)
	{
		GD.Print(damageType, " ", (int)damageType);
		damageActions[(int)damageType](entity, damage);
	}

	public static void DealPhysical(Entity entity, int damage)
	{
		entity.Damage(CalculatePhysical(entity, damage));
	}

	public static int CalculatePhysical(Entity entity, int damage)
	{
		return damage;
	}

	public static void DealFire(Entity entity, int damage)
	{
		entity.Damage(CalculateFire(entity, damage));
		entity.GetEffect<FrozenEffect>().End();
	}

	public static int CalculateFire(Entity entity, int damage)
	{
		float p = 1f;
		if (entity.Affected(EffectType.Wet))
		{
			p *= 0.5f;
		}
		if (entity.Affected(EffectType.Frozen))
		{
			p *= 1 << (int)entity.GetEffect<FrozenEffect>().Count;
		}
		return (int)Math.Ceiling(damage * p);
	}

	public static void DealFrost(Entity entity, int damage)
	{
		entity.Damage(CalculateFrost(entity, damage));
		if (entity.Affected(EffectType.Wet))
		{
			entity.GetEffect<WetEffect>().Conditions[0].End();
			new Condition(entity, new ConditionData(EffectType.Frozen, 60));
		}
	}

	public static int CalculateFrost(Entity entity, int damage)
	{
		float p = 1f;
		if (entity.Affected(EffectType.Wet))
		{
			p *= 2f;
		}
		return (int)Math.Ceiling(damage * p);
	}

	public static void DealThunder(Entity entity, int damage)
	{
		entity.Damage(CalculateThunder(entity, damage));
		if (entity.Affected(EffectType.Wet))
		{
			entity.GetEffect<WetEffect>().Conditions[0].End();
			new Condition(entity, new ConditionData(EffectType.Stunned, 60));
		}
		if (entity.Affected(EffectType.Wet))
		{
			entity.GetEffect<ElectrifiedEffect>().Conditions[0].End();
		}
	}

	public static int CalculateThunder(Entity entity, int damage)
	{
		float p = 1f;
		if (entity.Affected(EffectType.Electrified))
		{
			p *= 2f;
		}
		if (entity.Affected(EffectType.Wet))
		{
			p *= 2f;
		}
		return (int)Math.Ceiling(damage * p);
	}

	public static void DealEarth(Entity entity, int damage)
	{
		entity.Damage(CalculateEarth(entity, damage));
	}

	public static int CalculateEarth(Entity entity, int damage)
	{
		float p = 1f;
		if (entity.Affected(EffectType.Poisoned))
		{
			p *= 2f;
		}
		return (int)Math.Ceiling(damage * p);
	}
}
