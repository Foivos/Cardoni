namespace Cardoni;

using System.Collections.Generic;
using Godot;
using Godot.Collections;

public partial class MultipleCharacteristic : Characteristic<MultipleCharacteristicData>
{
	public List<Characteristic> Characteristics { get; set; } = new();

	public MultipleCharacteristic(Entity entity, MultipleCharacteristicData data)
		: base(entity, data)
	{
		foreach (CharacteristicData characteristicData in Data.Characteristics)
		{
			Characteristics.Add(characteristicData.Create(entity));
		}
	}

	public override void Start()
	{
		Characteristics.ForEach((characteristic) => characteristic.Start());
	}

	public override void End()
	{
		Characteristics.ForEach((characteristic) => characteristic.End());
	}
}
