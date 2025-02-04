using System;
using Godot;

public class Entity
{
    public int Health = 100;

    public int BaseSpeed = 10;
    public int Speed = 10;

    public string Name;

    public Effect[] Effects = new Effect[Enum.GetNames(typeof(EffectType)).Length];

    public void Damage(int damage)
    {
        Health -= damage;
        GD.Print(Name, " damaged at: ", GameState.Instance.Tick, " currecnt health is ", Health);
    }
}
