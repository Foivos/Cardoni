public enum Effect {
	Poisoned,
	SpeedModifier,
}

public interface IEffect {

	public Entity Entity { get; }

	public void End();
}
