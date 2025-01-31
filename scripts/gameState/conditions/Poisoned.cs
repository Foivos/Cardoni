

using Godot;

public class Poisoned: EffectCondition<PoisonedEffect> {
    public override EffectType EffectType => EffectType.Poisoned;

	public uint Strength;

	private Poisoned(Entity entity, uint strength, uint duration) {
		Expiring.Create(duration, OnExpire);

		Entity = entity;
		Strength = strength;

		if (Effect == null) {
			Effect = new PoisonedEffect(Entity, Strength);
		} else {
			Effect.Strength += Strength;
		}
	}


	public static void Apply(Entity entity, uint strength, uint duration) {
		new Poisoned(entity, strength, duration);
	}

	public void OnExpire() {
		Effect.Strength -= Strength;
		GD.Print("Poison Ended, at ", GameState.Instance.Tick," remaining stacks: ", Effect.Stacks);
		Effect.Update();
	}
}
