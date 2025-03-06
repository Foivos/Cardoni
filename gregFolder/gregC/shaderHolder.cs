using System;
using Godot;

public partial class shaderHolder : Node
{
	static shaderHolder Instance;

	public override void _Ready()
	{
		Instance = this;
	}

	[Export]
	ShaderMaterial _enemyShader,
		_outlineBlue,
		_targetingCustomOutline;

	public static ShaderMaterial enemyShader => Instance._enemyShader;
	public static ShaderMaterial outlineBlue => Instance._outlineBlue;
	public static ShaderMaterial targetingCustomOutline => Instance._targetingCustomOutline;
}
