namespace Cardoni;

using Godot;

[GlobalClass]
public partial class CardResource : Resource
{
	[Export]
	public string Name { get; set; }

	[Export]
	public string Description { get; set; }

	[Export]
	public int ManaCost { get; set; }

	[Export]
	public CardResult[] CardResults { get; set; }

	[Export]
	public Texture2D Sprite { get; set; }
}
