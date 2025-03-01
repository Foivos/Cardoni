using Godot;

namespace Cardoni;

public partial class Attack : TickedCharacteristic<AttackData>
{
	public const uint StacksPerAttack = 1200;

	public uint AttackStacks { get; set; }

	public uint StartingStacks => Data.StartingStacks;

	public EntityActive Active => Data.Active;

	public Entity Target { get; set; }

	public bool Attacking { get; set; }

	public Attack(Entity entity, AttackData data)
		: base(entity, data) { }

	public override void Tick()
	{
		if (Attacking)
		{
			if (Target != null && IsValidTarget(Target) && InRange(Target))
			{
				ContinueAttack();
			}
			else
			{
				FinishAttack();
			}
		}
		else
		{
			Target = FindClosestTarget();
			if (Target == null)
			{
				Entity.Direction = 0;
				return;
			}
			if (InRange(Target))
			{
				StartAttack();
			}
			else
			{
				Entity.Direction = Target.Y > Entity.Y ? 1 : -1;
			}
		}
	}

	protected virtual void FinishAttack()
	{
		AttackStacks += Entity.AttackSpeed;
		if (AttackStacks >= StartingStacks)
		{
			StopAttack();
			AttackStacks = StartingStacks;
		}
	}

	protected virtual bool InRange(Entity target)
	{
		return Data.AttackFilter.IsValid(Entity, target);
	}

	protected virtual void StartAttack()
	{
		Attacking = true;
		Entity.Direction = 0;
		AttackStacks = StartingStacks;
	}

	protected virtual void StopAttack()
	{
		Attacking = false;
	}

	protected virtual Entity FindClosestTarget()
	{
		Entity closest = null;
		int minDistance = Constants.TicksPerLane + 1;
		foreach (Entity entity in GameState.Entities)
		{
			if (!IsValidTarget(entity))
				continue;

			int distance = Entity.VerticalDistance(entity);
			if (distance < minDistance)
			{
				minDistance = distance;
				closest = entity;
			}
		}
		return closest;
	}

	protected virtual bool IsValidTarget(Entity entity)
	{
		return entity != Entity && entity.IsAlive && Data.TargetFilter.IsValid(Entity, entity);
	}

	public static Entity EntityAttackingNow;
	protected virtual void ContinueAttack()
	{
		AttackStacks += Entity.AttackSpeed;
		while (AttackStacks >= StacksPerAttack)
		{
			EntityAttackingNow = Entity;
			Active.Activate(Target);
			AttackStacks -= StacksPerAttack;
		}
	}
}
