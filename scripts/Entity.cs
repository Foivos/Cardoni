using System;
using Godot;

public class Entity {

	public int Health = 100;

	public int BaseSpeed = 10;
	public int Speed = 10;

	public IEffect[] Effects = new IEffect[Enum.GetNames(typeof(Effect)).Length];

	public void Damage(int damage) {
		GD.Print("Damaged at: ", GameState.Instance.Tick);
		Health -= damage;
	}
}
