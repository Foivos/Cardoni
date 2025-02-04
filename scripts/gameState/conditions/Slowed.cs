

using Godot;

public class Slowed: EffectCondition<SlowedEffect> {
    public override EffectType EffectType => EffectType.Slowed;

	public uint Modifier;

	private Slowed(Entity entity, uint modifier, uint duration) {
        new Expiring(duration, OnExpire);
        
		Entity = entity;
		Modifier = modifier;

		if ( Effect == null) {
			Effect = new SlowedEffect(Entity, Modifier);
		} else {
			Effect.Modifier += Modifier;
		}
	}

	public void OnExpire(uint tick) {
		Effect.Modifier -= Modifier;
		Effect.Update();
	}
}
