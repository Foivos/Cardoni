namespace Cardoni;

public abstract class Characteristic
{
	public abstract void Start();

	public abstract void End();
}

public abstract class Characteristic<T> : Characteristic
	where T : CharacteristicData
{
	public Entity Entity { get; set; }
	public T Data { get; set; }

	public Characteristic(Entity entity, T data)
	{
		Entity = entity;
		Data = data;
	}
}
