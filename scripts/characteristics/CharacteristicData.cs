namespace Cardoni;

using System;
using Godot;

[GlobalClass]
public abstract partial class CharacteristicData : Resource
{
	public abstract Characteristic Create(Entity entity);
}
