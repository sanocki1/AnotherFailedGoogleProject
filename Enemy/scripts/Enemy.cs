using Godot;
using System;

public partial class Enemy : PathFollow2D
{
	[Signal] public delegate void EnemyEscapedEventHandler();
	private int _enemyHealth = 100;
	public float Speed = 200f;
	public Guid EnemyID { get; private set; }
	private ProgressBar _healthBar;

	public override string ToString()
	{
		return $"Enemy ID: {EnemyID}";
	}

	public override void _Ready()
	{
		EnemyID = Guid.NewGuid();
		_healthBar = GetNode<ProgressBar>("HealthBar");
		_healthBar.Value = _enemyHealth;
	}
	public override void _PhysicsProcess(double delta)
	{
		SetProgress(GetProgress() + Speed * (float)delta);
		Rotate(3 * Mathf.Pi / 2);
	}

	public override void _Process(double delta)
	{
		if (ProgressRatio >= 0.99)
		{
			EmitSignal(SignalName.EnemyEscaped);
			QueueFree();
		}
	}

	private void OnAreaEntered(Bullet bullet)
	{
		TakeDamage(34);
		bullet.QueueFree();
	}

	public void TakeDamage(int damage)
	{
		_enemyHealth -= damage;
		_healthBar.Value = _enemyHealth;
		if (_enemyHealth <= 0)
		{
			QueueFree();
		}
	}
}