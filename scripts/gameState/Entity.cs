namespace Cardoni;

using System;
using Godot;

public partial class Entity : Area2D
{
	public int Health { get; set; }
	public int BaseSpeed = 10;
	public int Speed = 10;

	public string Name;

	public Effect[] Effects = new Effect[Enum.GetNames(typeof(EffectType)).Length];

	public Shape2D Shape { get; set; }

	public virtual void Damage(int damage)
	{
		Health -= damage;
		GD.Print(this, " damaged at: ", GameState.Instance.Tick, " currecnt health is ", Health);

		if (Health <= 0) {
			QueueFree();
			GameState.Instance.Entities.Remove(this);
		}
	}
}
