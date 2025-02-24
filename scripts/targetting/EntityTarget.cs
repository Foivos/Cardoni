namespace Cardoni;

using System.Collections.Generic;
using Godot;

[GlobalClass]
public abstract partial class EntityTarget : CardTarget
{
	[Export]
	public EntityMask EntityMask { get; set; } = new();

	public abstract List<Entity> Targets();
}
