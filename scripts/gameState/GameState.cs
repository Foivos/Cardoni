namespace Cardoni;

using System.Collections.Generic;
using Godot;

public partial class GameState : Node
{
	public static GameState Instance { get; private set; }
	public uint Tick;

	public const uint TicksPerSecond = 20;
	public const double secondsPerTick = 1.0 / TicksPerSecond;

	public Cardonio.PriorityQueue<Expiring> ExpiringQueue = new Cardonio.PriorityQueue<Expiring>();

	public List<ITicked> Ticked = new List<ITicked>();

	public const int LanesNumber = 4;

	public List<Entity> Entities = new List<Entity>();

	CardState CardState = new CardState();

	public static Card SelectedCard
	{
		get { return Instance.CardState.Selected; }
		set { Instance.CardState.Selected = value; }
	}

	public int Mana
	{
		get { return (int)(ManaStacks / StacksPerMana); }
		set { ManaStacks = (int)(ManaStacks % StacksPerMana + (value * StacksPerMana)); }
	}

	public int ManaStacks = 0;

	public const uint StacksPerMana = 1200;

	public int StacksPerTick = 60;

	public override void _Ready()
	{
		Instance = this;

		new Ticked(
			(uint tick) =>
			{
				ManaStacks += StacksPerTick;
			}
		);
	}

	public override void _PhysicsProcess(double dt)
	{
		for (int i = 0; i < Ticked.Count; i++)
		{
			Ticked[i].Tick(Tick);
		}

		while (ExpiringQueue.Count > 0 && ExpiringQueue.Top.End <= Tick)
		{
			Expiring expiring = ExpiringQueue.Pop();
			expiring.OnExpire(Tick);
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

		Tick++;
	}

	public void AddTicked(ITicked ticked)
	{
		Ticked.Add(ticked);
	}

	public void RemoveTicked(ITicked ticked)
	{
		Ticked.Remove(ticked);
	}

	public void AddExpiring(Expiring expiring)
	{
		ExpiringQueue.Push(expiring);
	}

	public void RemoveExpiring(Expiring expiring)
	{
		ExpiringQueue.Remove(expiring);
	}
}
