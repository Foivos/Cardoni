namespace Cardoni;

using Godot;

public partial class SpriteData : Resource
{
	public SpriteData() { }

	[Export]
	public Texture2D Texture { get; set; }

	[Export]
	public Rect2 RegionRect { get; set; }
}
