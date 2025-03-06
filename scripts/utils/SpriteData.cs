namespace Cardoni;

using Godot;

[GlobalClass]
public partial class SpriteData : Resource
{
	public SpriteData() { }

	[Export]
	public Texture2D Texture { get; set; }

	[Export]
	public bool regionDisabled;

	[Export]
	public Rect2 RegionRect { get; set; }

	public void setUp(Sprite2D sprite) // GREGORY
	{
		if (Texture == null || sprite == null)
		{
			GD.PushError("NULL sometning");
			return;
		}

		sprite.Texture = Texture;
		sprite.RegionEnabled = !regionDisabled;

		if (!regionDisabled)
			sprite.RegionRect = RegionRect;
	}
}
