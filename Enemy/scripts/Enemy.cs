using Godot;
using System;

public partial class Enemy : RigidBody2D
{
	private int _enemyHealth = 100;
	public float Progress = 0;
	public float Speed = 100f;
	public Guid EnemyID { get; private set; }
	private ProgressBar _healthBar;

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
		_healthBar = GetNode<ProgressBar>("HealthBar");
		_healthBar.Value = _enemyHealth;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Progress += Speed;
		_healthBar.Value = _enemyHealth;
	}

	public void TakeDamage(int damage)
	{
		_enemyHealth -= damage;
		if (_enemyHealth <= 0)
		{
			QueueFree();
		}
	}

	// Call this when enemy leave the screen (remember to connect signal!!!)
	private void OnVisibleOnScreenNotifier2DScreenExited()
	{
		QueueFree();
	}
}
