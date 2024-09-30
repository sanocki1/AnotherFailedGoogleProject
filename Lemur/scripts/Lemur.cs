using Godot;
using System;
using System.Collections.Generic;
public partial class Lemur : StaticBody2D
{
	private Dictionary<Guid, Enemy> enemyPathFollowMap = new Dictionary<Guid, Enemy>();
	private PackedScene bullet = GD.Load<PackedScene>("res://Bullet/scenes/Bullet.tscn");
	private float _shootTimer = 0f;


	private void OnLemurRangeAreaEntered(Area2D enemy)
	{
		Enemy parent = (Enemy)enemy.GetParent();
		// GD.Print("Lemur sees enemy " + enemy.Name);
		// GD.Print("Enemy parent is " + parent.Name);
		this.enemyPathFollowMap.Add(parent.EnemyID, parent);
	}

	private void OnLemurRangeAreaExited(Area2D enemy)
	{
		Enemy parent = (Enemy)enemy.GetParent();
		// GD.Print("Lemur lost sight of enemy " + enemy.Name);
		// GD.Print("Enemy parent is " + parent.Name);
		this.enemyPathFollowMap.Remove(parent.EnemyID);
	}

	public override void _Ready()
	{
	}

	public override void _PhysicsProcess(double delta)
	{
		if (enemyPathFollowMap.Count > 0)
		{
			float max_progress = 0;
			Enemy maxDistanceEnemy = null;

			foreach (var enemy in enemyPathFollowMap.Values)
			{
				if (enemy.getProgress() > max_progress)
				{
					max_progress = enemy.getProgress();
					maxDistanceEnemy = enemy;
				}
			}

			LookAt(maxDistanceEnemy.GlobalPosition);
			_shootTimer -= (float)delta;

			if (_shootTimer <= 0f)
			{
				_shootTimer = 1f;
				Vector2 direction = new Vector2(Mathf.Cos(Rotation), Mathf.Sin(Rotation)).Normalized();
				ShootBullet(direction);
			}
		}
	}

	public void ShootBullet(Vector2 direction)
	{
		var newBullet = (Bullet)bullet.Instantiate();
		newBullet.Position = GlobalPosition;
		newBullet.Direction = direction;
		GetParent().AddChild(newBullet);
	}
}
