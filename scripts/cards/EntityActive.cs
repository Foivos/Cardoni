namespace Cardoni;

using Godot;

[GlobalClass]
public abstract partial class EntityActive : Active
{
	public EntityActive()
		: base() { }

	public abstract void Activate(Entity entity);
}
