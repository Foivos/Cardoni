namespace Cardoni;

using Godot;

[GlobalClass]
public partial class AlwaysFilter : EntityFilter
{
	public override bool IsValid(Entity source, Entity other)
	{
		return true;
	}
}
