namespace Cardoni;

using Godot;

[GlobalClass]
public partial class CardData : Resource
{
	[Export]
	public string Name { get; set; }

	[Export]
	public string Description { get; set; }

	[Export]
	public int ManaCost { get; set; }

	[Export]
	public SpriteData Sprite { get; set; }

	[Export]
	public CardResult CardResult { get; set; }
}
