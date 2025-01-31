using Godot;
using System.Collections.Generic;

public partial class GameState: Node
{
	public static GameState Instance { get; private set; }
	public uint Tick;

	public const uint TicksPerSecond = 20;
	public const double secondsPerTick = 1.0 / TicksPerSecond;

	public PriorityQueue<Condition> Conditions = new PriorityQueue<Condition>();

	public List<ITicked> Ticked = new List<ITicked>();

	private Entity Entity = new Entity();

	public override void _Ready()
	{
		Instance = this;		
	}
	public override void _PhysicsProcess(double dt) {
		for (int i = 0; i < Ticked.Count; i++) {
			Ticked[i].Tick(Tick);
		}
		while (Conditions.Count > 0 && Conditions.Top. Ends <= Tick) {
			Condition condition = Conditions.Pop();
			condition.End();
		}
		
		Tick++;
	}

	public void AddTicked(ITicked ticked) {
		Ticked.Add(ticked);
	}

	public void RemoveTicked(ITicked ticked) {
		Ticked.Remove(ticked);
	}

	public void AddCondition(Condition condition) {
		Conditions.Push(condition);
	}
}
