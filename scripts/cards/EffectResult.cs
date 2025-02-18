namespace Cardoni;

using System.Collections.Generic;
using System.Linq;
using Godot;
using Godot.Collections;

[GlobalClass]
public partial class EffectResult : CardResult
{
	[Export]
	public EntityTarget EntityTarget { get; set; }

	[Export]
	public EntityActive Active { get; set; }

	public override CardTarget Target => EntityTarget;

	public EffectResult() { }

	public virtual bool Affects(Entity entity)
	{
		return true;
	}

	public virtual void Affect(Entity entity)
	{
		Active.Activate(entity);
	}

	public override void Activate()
	{
		foreach (Entity entity in EntityTarget.Targets())
		{
			Affect(entity);
		}
	}
}
