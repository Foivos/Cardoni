namespace Cardoni;

using Godot;

[GlobalClass]
public partial class NotFilter : EntityFilter
{
	[Export]
	public EntityFilter Filter { get; set; }

	public override bool IsValid(Entity source, Entity other)
	{
		return !Filter.IsValid(source, other);
	}
}
