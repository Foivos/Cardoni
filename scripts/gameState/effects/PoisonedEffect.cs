namespace Cardoni;

public class PoisonedEffect : Effect, ITicked
{
    public override EffectType EffectType => EffectType.Poisoned;

    public const uint StacksPerHealth = 1200;

    public uint Strength = 0;
    public uint Stacks = 0;

    public bool Ticking = false;

    public PoisonedEffect(Entity entity, uint strength)
    {
        Strength = strength;
        Entity = entity;

        RefEffect = this;
        Update();
    }

    public void Tick(uint tick)
    {
        Stacks += Strength;
        if (Stacks >= StacksPerHealth)
        {
            int damage = (int)(Stacks / StacksPerHealth);
            Stacks = Stacks % StacksPerHealth;
            Entity.Damage(damage);
        }
    }

    public override void Update()
    {
        if (Ticking && Strength == 0)
        {
            Ticking = false;
            GameState.Instance.RemoveTicked(this);
        }
        else if (!Ticking && Strength > 0)
        {
            Ticking = true;
            GameState.Instance.AddTicked(this);
        }
    }
}
