namespace Cardoni;

using Godot;

[GlobalClass]
public abstract partial class EntityFilter : Resource
{
	public abstract bool IsValid(Entity source, Entity other);
}
