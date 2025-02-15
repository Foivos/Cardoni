namespace Cardoni;

using Godot;
using Godot.Collections;

public partial class MultipleCharacteristicData : CharacteristicData
{
	[Export]
	public Array<CharacteristicData> Characteristics { get; set; } = new();

	public override Characteristic Create(Entity entity)
	{
		return new MultipleCharacteristic(entity, this);
	}
}
