namespace Cardoni;

public abstract class CardTarget
{
	public ICardEffect[] CardEffects { get; set; }

	public void Activate()
	{
		foreach (ICardEffect effect in CardEffects)
		{
			if (effect is Summoning) { }
			else if (effect is Affecting && this is EntityTarget)
			{
				foreach (Entity entity in ((EntityTarget)this).Targets())
				{
					Affecting affecting = (Affecting)effect;
					if (!affecting.Affects(entity))
						continue;

					affecting.Affect(entity);
				}
			}
		}
	}
}
