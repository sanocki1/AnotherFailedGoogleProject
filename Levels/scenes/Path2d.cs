using Godot;
using System;

public partial class Path2d : Path2D
{
	private float _timer = 0f;
	[Export] public float SpawnTime = 5f;
	
	private PackedScene _follower = (PackedScene)GD.Load("res://Levels/scenes/path_follow_2d.tscn");
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_timer += (float)delta;

		if (_timer > SpawnTime)
		{
			var newFollower = (Node2D)_follower.Instantiate();
			AddChild(newFollower);
			_timer = 0;
		}
	}
}
