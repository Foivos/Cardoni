namespace Cardoni;

using Godot;

[GlobalClass]
public partial class OrFilter : EntityFilter
{
	[Export]
	public EntityFilter Filter1 { get; set; }

	[Export]
	public EntityFilter Filter2 { get; set; }

	public override bool IsValid(Entity source, Entity other)
	{
		return Filter1.IsValid(source, other) || Filter2.IsValid(source, other);
	}
}
