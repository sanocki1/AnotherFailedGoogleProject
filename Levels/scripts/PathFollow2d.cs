using Godot;
using System;

public partial class PathFollow2d : PathFollow2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		SetProgress(GetProgress() + 60 * (float)delta);
		GetChild<Enemy>(0).RotationDegrees = 270;
	}
}
