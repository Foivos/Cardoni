using Godot;
using System.Collections.Generic;

public partial class GameState: Node
{
	public static GameState Instance { get; private set; }
	public uint Tick;

	public const uint TicksPerSecond = 20;
	public const double secondsPerTick = 1.0 / TicksPerSecond;

	public PriorityQueue<Expiring> ExpiringQueue = new PriorityQueue<Expiring>();

	public List<ITicked> Ticked = new List<ITicked>();

	public override void _Ready()
	{
		Instance = this;	

		Entity entity1 = new Entity();
		Entity entity2 = new Entity();
		Entity entity3 = new Entity();
		entity1.Name = "1";
		entity2.Name = "2";
		entity3.Name = "3";

		Expiring.Create(3, new System.Action(() => {
			GD.Print("expired1");
			Poisoned.Apply(entity1, 10, 12);
			Poisoned.Apply(entity2, 10, 12);
			Poisoned.Apply(entity3, 10, 12);
		}));


		Expiring.Create(8, new System.Action(() => {
			GD.Print("expired2");
			Poisoned.Apply(entity1, 2, 120);
			Poisoned.Apply(entity2, 3, 80);
			Poisoned.Apply(entity3, 4, 60);
		}));


		Expiring.Create(18, new System.Action(() => {
			GD.Print("expired3");
			Poisoned.Apply(entity1, 12, 20);
			Poisoned.Apply(entity2, 15, 16);
			Poisoned.Apply(entity3, 20, 12);
		}));
	}
	public override void _PhysicsProcess(double dt) {
		for (int i = 0; i < Ticked.Count; i++) {
			Ticked[i].Tick(Tick);
		}
		while (ExpiringQueue.Count > 0 && ExpiringQueue.Top.End <= Tick) {
			Expiring expiring = ExpiringQueue.Pop();
			expiring.OnExpire();
		}

		Tick++;
	}

	public void AddTicked(ITicked ticked) {
		Ticked.Add(ticked);
	}

	public void RemoveTicked(ITicked ticked) {
		Ticked.Remove(ticked);
	}

	public void AddExpiring(Expiring expiring) {
		ExpiringQueue.Push(expiring);
	}
}
