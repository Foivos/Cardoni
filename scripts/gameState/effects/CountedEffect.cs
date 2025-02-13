using System;

namespace Cardoni;

public abstract class CountedEffect : Effect
{
	int count;
	public int Count
	{
		get => count;
		set
		{
			if (value < 0)
			{
				count = 0;
			}
			else
			{
				count = value;
			}
		}
	}
	public bool Active { get; set; }

	public CountedEffect(Entity entity)
	{
		Entity = entity;
		Count = 1;
		Update();

		RefEffect = this;
	}

	public override bool Affected()
	{
		return Active;
	}

	public override void Update()
	{
		if (Active && Count == 0)
		{
			Remove();
			Active = false;
		}
		else if (!Active && Count != 0)
		{
			Apply();
			Active = true;
		}
	}

	protected abstract void Apply();

	protected abstract void Remove();
}
