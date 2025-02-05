namespace Cardoni;

using Godot;

public partial class CardArea: Area2D {
	public int Index { get; set; }
	public CardView CardView { get; set; }

	public override void _Ready() {
		InputEvent += _Input;
	}


	public void _Input(Node viewport, InputEvent @event, long shapeIdx)
	{
		CardView.EventInput(Index, @event);
	}
}
