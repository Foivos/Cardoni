namespace Cardoni;

using System;
using Godot;

public abstract partial class TickedCharacteristic<T> : Characteristic<T>
	where T : CharacteristicData
{
	public TickedCharacteristic(Entity entity, T data)
		: base(entity, data) { }

	public abstract void Tick();

	public override void Start()
	{
		GameState.AddTicked(Tick);
	}

	public override void End()
	{
		GameState.RemoveTicked(Tick);
	}
}
