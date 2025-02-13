namespace Cardoni;

using System.Collections.Generic;
using System.Linq;
using Godot;

public class EffectResult : CardResult
{
	public EffectResult(List<EntityTarget> targets, List<EntityActive> effects)
	{
		Targets = targets.Cast<CardTarget>().ToList();
		Effects = effects.Cast<IActive>().ToList();
	}

	public virtual bool Affects(Entity entity)
	{
		return true;
	}

	public virtual void Affect(Entity entity)
	{
		foreach (IActive effect in Effects)
		{
			((EntityActive)effect).Activate(entity);
		}
	}

	public virtual List<Entity> GetTargets()
	{
		List<Entity> targets = new();
		foreach (CardTarget target in Targets)
		{
			targets.AddRange(((EntityTarget)target).Targets());
		}
		return targets.Distinct().ToList();
	}

	public override void Activate()
	{
		foreach (Entity entity in GetTargets())
		{
			GD.Print(entity.Effects);
			Affect(entity);
		}
	}
}
