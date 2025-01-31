public abstract class Effect {

	public Entity Entity;

	public abstract EffectType EffectType { get; }

	public Effect RefEffect {
		get {
			return Entity.Effects[(int)EffectType];
		}
		set {
			Entity.Effects[(int)EffectType] = value;
		}
	}

	public virtual void Update() {}
}
