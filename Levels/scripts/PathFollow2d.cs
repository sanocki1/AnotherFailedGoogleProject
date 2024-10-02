using Godot;
using System;

public partial class PathFollow2d : PathFollow2D
{
	private HealthbarHud _healthbarHud;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_healthbarHud = GetNode<HealthbarHud>("../../LevelHud/HealthbarHud");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		SetProgress(GetProgress() + 300 * (float)delta);
		GetChild<Enemy>(0).RotationDegrees = 270;

		if (ProgressRatio >= 0.99)
		{
			EnemyEscaped();
		}
	}

	private void EnemyEscaped()
	{
		QueueFree();
		_healthbarHud.ApplyDamage(25);
	}
}
