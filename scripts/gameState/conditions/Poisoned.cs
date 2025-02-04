using Godot;
using State;

public class Poisoned: EffectCondition<PoisonedEffect> {
	public override EffectType EffectType => EffectType.Poisoned;

    public uint Strength;

    private Poisoned(Entity entity, uint strength, uint duration)
    {
        new Expiring(duration, OnExpire, 1);

        Entity = entity;
        Strength = strength;

        if (Effect == null)
        {
            Effect = new PoisonedEffect(Entity, Strength);
        }
        else
        {
            Effect.Strength += Strength;
        }
    }

    public static void Apply(Entity entity, uint strength, uint duration)
    {
        new Poisoned(entity, strength, duration);
    }

    public void OnExpire(uint tick)
    {
        Effect.Strength -= Strength;
        GD.Print(
            "Poison Ended, at ",
            GameState.Instance.Tick,
            " remaining stacks: ",
            Effect.Stacks
        );
        Effect.Update();
    }
}
