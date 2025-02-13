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

	List<Entity> entities = new();

	public static List<Entity> Entities => Instance.entities;

	List<Entity> dying = new();

	public static List<Entity> Dying => Instance.dying;

	List<Entity> spawning = new();

	public static List<Entity> Spawning => Instance.spawning;

	List<Projectile> projectiles = new();

	public static List<Projectile> Projectiles => Instance.projectiles;

	List<Projectile> spawningProjectiles = new();

	public static List<Projectile> SpawningProjectiles => Instance.spawningProjectiles;

	List<Projectile> dyingProjectiles = new();

	public static List<Projectile> DyingProjectiles => Instance.dyingProjectiles;

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
		SpawnEntities();

		MoveEntities();

		ProcessTicked();

		SpawnProjectiles();

		ProcessProjectiles();
		
		KillProjectiles();

		ProcessExpiring();

		KillEntities();

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

	public static void Kill(Entity entity)
	{
		Instance.dying.Add(entity);
	}

	private void SpawnEntities()
	{
		foreach (Entity entity in spawning)
		{
			entity.Spawn();
			entities.Add(entity);
			Events.InvokeSpawn(entity);
		}
		spawning = new();
	}

	private void MoveEntities()
	{
		foreach (Entity entity in Entities)
		{
			entity.Move();
		}
	}

	private void ProcessTicked()
	{
		for (int i = 0; i < ticked.Count; i++)
		{
			ticked[i]();
		}
	}

	private void ProcessExpiring()
	{
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
	}

	private void KillEntities()
	{
		foreach (Entity entity in dying)
		{
			GD.Print("Killing ", entity.Name, " ", entities.Count);
			entity.Kill();
			entities.Remove(entity);
		}
		dying = new();
	}

	private void ProcessProjectiles() {
		foreach(Projectile projectile in projectiles) {
			projectile.Move();
			Entity entity;
			while((entity = projectile.Colliding()) != null) {
				projectile.Hit(entity);
			}
		}
	}
	private void SpawnProjectiles() {
		foreach(Projectile projectile in spawningProjectiles) {
			projectiles.Add(projectile);
		}
		spawningProjectiles = new();
	}
	private void KillProjectiles() {
		foreach(Projectile projectile in dyingProjectiles) {
			projectiles.Remove(projectile);
		}
		dyingProjectiles = new();
	}
}
