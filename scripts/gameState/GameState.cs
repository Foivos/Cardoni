namespace Cardoni;

using System;
using System.Collections.Generic;
using Godot;

public partial class GameState : Node
{
	public static GameState Instance { get; private set; }
	uint tick;

	public static uint Tick => Instance.tick;

	public static int TicksPerSecond => Engine.PhysicsTicksPerSecond;
	public static double SecondsPerTick => 1.0 / TicksPerSecond;

	readonly PriorityQueue<Expiring> expiringQueue = new();

	readonly List<Action> ticked = new();

	public const int LanesNumber = 4;

	public List<Entity> entities = new();

	public static List<Entity> Entities => Instance.entities;

	readonly CardState CardState = new();

	ulong lastTick;

	public static Card SelectedCard
	{
		get { return Instance.CardState.Selected; }
		set { Instance.CardState.Selected = value; }
	}

	public int Mana
	{
		get { return (int)(manaStacks / StacksPerMana); }
		set { manaStacks = (int)(manaStacks % StacksPerMana + (value * StacksPerMana)); }
	}

	public int manaStacks = 0;

	public const uint StacksPerMana = 1200;

	public int StacksPerTick = 60;

	public override void _Ready()
	{
		Instance = this;

		AddTicked(() =>
		{
			manaStacks += StacksPerTick;
		});
	}

	public override void _PhysicsProcess(double dt)
	{
		foreach (Entity entity in Entities)
		{
			entity.Move();
		}

		for (int i = 0; i < ticked.Count; i++)
		{
			ticked[i]();
		}

		while (expiringQueue.Count > 0 && expiringQueue.Top.End <= Tick)
		{
			Expiring expiring = expiringQueue.Pop();
			expiring.OnExpire();
			if (expiring.Repeat != 1)
			{
				if (expiring.Repeat > 1)
				{
					expiring.Repeat--;
				}
				expiring.End = Tick + expiring.Duration;
				AddExpiring(expiring);
			}
		}

		tick++;
		lastTick = Time.GetTicksMsec();
	}

	public override void _Process(double _)
	{
		float dt = (float)(Time.GetTicksMsec() - lastTick) / 1000f * TicksPerSecond;
		foreach (Entity entity in Entities)
		{
			entity.UpdatePosition(dt);
		}
	}

	public static void AddTicked(Action ticked)
	{
		Instance.ticked.Add(ticked);
	}

	public static void RemoveTicked(Action ticked)
	{
		Instance.ticked.Remove(ticked);
	}

	public static void AddExpiring(Expiring expiring)
	{
		Instance.expiringQueue.Push(expiring);
	}

	public static void RemoveExpiring(Expiring expiring)
	{
		Instance.expiringQueue.Remove(expiring);
	}
}
