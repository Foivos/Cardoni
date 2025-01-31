public class SpeedModifierEffect: IEffect {

	public IEffect RefEffect {
		get {
			return Entity.Effects[(int)Effect.Poisoned];
		}
		set {
			Entity.Effects[(int)Effect.Poisoned] = value;
		}
	}

	public Entity Entity { get; set; }

	private int modifier = 0;

	public int Modifier {
		get {
			return modifier;
		}
		set {
			modifier = value;
			Entity.Speed = Entity.BaseSpeed + modifier;
		}
	}

	public SpeedModifierEffect(Entity entity, int modifier) {
		Modifier = modifier;
		Entity = entity;

		RefEffect = this;
	}

	public void End() {
		RefEffect = null;
	}
}
