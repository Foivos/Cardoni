using System;
using Godot;
using State;

public partial class Card : Node2D
{
    public uint Index;
    public bool selected;
    public string description;
    public int manaCost;

    [Export]
    public Sprite2D mySprite;

    [Export]
    public RichTextLabel myText;

    public override void _Ready()
    {
        SetPosition();
    }

    public void SetPosition() { }
}
