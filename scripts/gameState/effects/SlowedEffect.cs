namespace Cardoni;

public class SlowedEffect : Effect
{
	public override EffectType EffectType => EffectType.Slowed;

	private uint modifier = 0;

	public uint Modifier
	{
		get { return modifier; }
		set
		{
			modifier = value;
			Entity.Speed = Entity.BaseSpeed - (int)modifier;
		}
	}

	public SlowedEffect(Entity entity, uint modifier)
	{
		Modifier = modifier;
		Entity = entity;

		RefEffect = this;
	}
}
