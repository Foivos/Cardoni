

using Godot;

public class Poisoned: Condition {

	public uint Strength;

	public Entity Entity;

	public PoisonedEffect RefEffect {
		get {
			return (PoisonedEffect) Entity.Effects[(int)Effect.Poisoned];
		}
		set {
			Entity.Effects[(int)Effect.Poisoned] = value;
		}
	}

	public Poisoned(Entity entity, uint strength, uint duration) {
		Ends = GameState.Instance.Tick + duration;
		Entity = entity;
		Strength = strength;

		if (RefEffect == null) {
			RefEffect = new PoisonedEffect(Entity, Strength);
		} else {
			RefEffect.Strength += Strength;
		}
	}

	public override void End() {
		RefEffect.Strength -= Strength;
		GD.Print("Ended, at ", GameState.Instance.Tick," remaining stacks: ", RefEffect.Stacks);
		if (RefEffect.Strength == 0) {
			RefEffect.End();
		}
	}
}
