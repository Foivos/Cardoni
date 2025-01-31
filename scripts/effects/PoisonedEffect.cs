public class PoisonedEffect: IEffect, ITicked {

	public IEffect RefEffect {
		get {
			return Entity.Effects[(int)Effect.Poisoned];
		}
		set {
			Entity.Effects[(int)Effect.Poisoned] = value;
		}
	}

	public Entity Entity { get; set; }

	public const uint StacksPerHealth = 120;

	public uint Strength = 0;
	public uint Stacks = 0;

	public PoisonedEffect(Entity entity, uint strength) {
		Strength = strength;
		Entity = entity;

		RefEffect = this;
		GameState.Instance.AddTicked(this);
	}

	public void Tick(uint tick) {
		Stacks += Strength;
		if (Stacks >= StacksPerHealth) {
			int damage = (int) (Stacks / StacksPerHealth);
			Stacks = Stacks % StacksPerHealth;
			Entity.Damage(damage);
		}
	}

	public void End() {
		RefEffect = null;
		GameState.Instance.RemoveTicked(this);
	}
}
