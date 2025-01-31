

using Godot;

public class SpeedModifier: Condition {

	public int Modifier;

	public Entity Entity;

	public SpeedModifierEffect RefEffect {
		get {
			return (SpeedModifierEffect) Entity.Effects[(int)Effect.SpeedModifier];
		}
		set {
			Entity.Effects[(int)Effect.SpeedModifier] = value;
		}
	}

	public SpeedModifier(Entity entity, int modifier, uint duration) {
		Ends = GameState.Instance.Tick + duration;
		Entity = entity;
		Modifier = modifier;

		if (RefEffect == null) {
			RefEffect = new SpeedModifierEffect(Entity, Modifier);
		} else {
			RefEffect.Modifier += Modifier;
		}
	}

	public override void End() {
		RefEffect.Modifier -= Modifier;
		if (RefEffect.Modifier == 0) {
			RefEffect.End();
		}
	}
}
