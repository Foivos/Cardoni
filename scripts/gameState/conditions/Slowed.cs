

using Godot;

public class Slowed: EffectCondition<SlowedEffect> {
    public override EffectType EffectType => EffectType.Slowed;

	public uint Modifier;

	private Slowed(Entity entity, uint modifier, uint duration) {
        Expiring.Create(duration, OnExpire);
        
		Entity = entity;
		Modifier = modifier;

		if ( Effect == null) {
			Effect = new SlowedEffect(Entity, Modifier);
		} else {
			Effect.Modifier += Modifier;
		}
	}

	public void OnExpire() {
		Effect.Modifier -= Modifier;
		Effect.Update();
	}
}
