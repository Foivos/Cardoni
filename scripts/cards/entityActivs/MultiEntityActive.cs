namespace Cardoni;

using Godot;

[GlobalClass]
public partial class MultiEntityActive : EntityActive
{
	[Export]
	public EntityActive[] Actives { get; set; }

	public MultiEntityActive()
		: base() { }

	public override void Activate(Entity entity)
	{
		GD.Print("Activate START");

		foreach (EntityActive active in Actives)
		{
			active.Activate(entity);
		}
		

		GD.Print("Activate AFTER");
	}
}
