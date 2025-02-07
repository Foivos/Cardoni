namespace Cardoni;

using System;
using System.Collections.Generic;
using Godot;

public partial class Entity : Area2D
{
	public int Health { get; set; }
	public int BaseSpeed = 10;
	public int Speed = 10;

	public string Name;

	public Effect[] Effects = new Effect[Enum.GetNames(typeof(EffectType)).Length];
	public List<ICondition> Conditions = new List<ICondition>();

	public Shape2D Shape { get; set; }

	public virtual void Damage(int damage)
	{
		Health -= damage;
		GD.Print(this, " damaged at: ", GameState.Instance.Tick, " currecnt health is ", Health);

		if (Health <= 0)
		{
			Kill();
		}
	}

	public virtual void Kill()
	{
		foreach (Effect effect in Effects)
		{
			if (effect == null)
			{
				continue;
			}
			effect.End();
		}
		foreach (ICondition condiiton in Conditions)
		{
			condiiton.End();
		}
		GameState.Instance.Entities.Remove(this);
		QueueFree();
	}
}
