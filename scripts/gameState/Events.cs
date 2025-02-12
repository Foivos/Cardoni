namespace Cardoni;

public class Events
{
	public delegate void OnSpawnDelegate(Entity entity);

	public static event OnSpawnDelegate OnSpawn;

	public static void InvokeSpawn(Entity entity)
	{
		OnSpawn?.Invoke(entity);
	}
}
