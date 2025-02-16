namespace Cardoni;

using Godot;

[GlobalClass]
public partial class EntityData : Resource
{
	public EntityData() { }

	[Export]
	public string Name = "Goblin Warrior";

	[Export]
	public uint Width { get; set; } = 1;

	[Export]
	public uint Height { get; set; } = Constants.GridTicks;

	[Export]
	public uint MaxHealth { get; set; }

	[Export]
	public EntityMask Mask { get; set; } = new EntityMask(new EntityMasks[] { EntityMasks.Enemy });

	[Export]
	public uint BaseMovementSpeed { get; set; } = 60;

	[Export]
	public uint BaseAttackSpeed { get; set; } = 40;

	[Export]
	public SpriteData Sprite { get; set; }

	[Export]
	public CharacteristicData Characteristic { get; set; }
}
