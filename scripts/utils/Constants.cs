public class Constants
{
	public const int NumberOfLanes = 4;
	public const float GridWidth = 100;
	public const float GridHeight = 100;

	public const int GridTicks = 1200;

	public const int NumberOfRows = 6;

	public static int TicksPerLane => NumberOfRows * GridTicks;
}
