namespace Cardoni;

using Godot;

[GlobalClass]
public abstract partial class CooldownActive : Resource
{
	public abstract void Activate(Entity entity);
}
