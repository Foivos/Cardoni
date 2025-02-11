namespace Cardoni;

public class HasteEffect : Effect
{

    public uint Count { get; set; }
    bool active;


	public override EffectType EffectType => EffectType.Poisoned;

	public HasteEffect(Entity entity)
	{
		Entity = entity;
        Count = 1;
        Update();

		RefEffect = this;
	}

	public override void End()
	{
		base.End();
        Entity.MovementSpeedModifier -= 0.5f;
        Entity.AttackSpeedModifier -= 0.5f;
	}

	public override void Update()
	{
		if ( active && Count == 0 ) {
            Entity.MovementSpeedModifier -= 0.5f;
            Entity.AttackSpeedModifier -= 0.5f;
            active = false;
        } else if (!active && Count != 0) {
            Entity.MovementSpeedModifier += 0.5f;
            Entity.AttackSpeedModifier += 0.5f;
            active = true;
        }
	}
}
