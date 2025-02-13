namespace Cardoni;

using System;
using Godot;

public partial class EntityParent : Area2D
{
	[Export]
	public TextureProgressBar HealthBar;

	[Export]
	public Sprite2D Sprite;

	[Export]
	public Sprite2D Weapon;

}
