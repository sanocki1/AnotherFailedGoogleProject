using Godot;
using System;

public partial class Enemy : RigidBody2D
{
	int enemyHealth = 100;
	public float Progress = 0;
	public float Speed = 100f;
	public Guid EnemyID { get; private set; }

	public float getProgress()
	{
		return Progress;
	}

	public override string ToString()
	{
		return $"Enemy ID: {EnemyID}";
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		EnemyID = Guid.NewGuid();
		GD.Print("Enemy created with UUID: " + EnemyID.ToString());
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Progress += (float)delta * Speed;
	}

	// Call this when enemy leave the screen (remember to connect signal!!!)
	private void OnVisibleOnScreenNotifier2DScreenExited()
	{
		QueueFree();
	}
}
