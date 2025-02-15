namespace Cardoni;

using System.Collections.Generic;
using Godot;

[GlobalClass]
public partial class MultiEntityTarget : EntityTarget
{
	[Export]
	public EntityTarget[] EntityTargets { get; set; }

	public MultiEntityTarget()
		: base() { }

	public override List<Entity> Targets()
	{
		List<Entity> list = new();
		foreach (EntityTarget target in EntityTargets)
		{
			list.AddRange(target.Targets());
		}
		return list;
	}
}
