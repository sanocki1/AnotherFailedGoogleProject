using Godot;
using System;

public partial class Enemy : RigidBody2D
{
	int enemyHealth = 100;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	// Call this when enemy leave the screen (remember to connect signal!!!)
	private void OnVisibleOnScreenNotifier2DScreenExited()
	{
		QueueFree();
	}
}
