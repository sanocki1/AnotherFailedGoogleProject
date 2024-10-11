using Godot;
using System;
using System.Collections.Generic;
public partial class Lemur : StaticBody2D
{
	private Dictionary<Guid, Enemy> _enemiesInRange = new Dictionary<Guid, Enemy>();
	private PackedScene bullet = GD.Load<PackedScene>("res://Bullet/scenes/Bullet.tscn");
	private float _shootTimer = 0f;
	private float _shootDelay = 1f;


	private void OnLemurRangeAreaEntered(Area2D enemy)
	{
		Enemy parent = (Enemy)enemy.GetParent();
		_enemiesInRange.Add(parent.EnemyID, parent);
	}

	private void OnLemurRangeAreaExited(Area2D enemy)
	{
		Enemy parent = (Enemy)enemy.GetParent();
		_enemiesInRange.Remove(parent.EnemyID);
	}

	public override void _PhysicsProcess(double delta)
	{
		_shootTimer = Mathf.Max(0f, _shootTimer - (float)delta);

		if (_enemiesInRange.Count > 0)
		{
			float max_progress = 0;
			Enemy maxDistanceEnemy = null;

			foreach (var enemy in _enemiesInRange.Values)
			{
				if (enemy.Progress > max_progress)
				{
					max_progress = enemy.Progress;
					maxDistanceEnemy = enemy;
				}
			}

			LookAt(maxDistanceEnemy.GlobalPosition);

			if (_shootTimer == 0f)
			{
				_shootTimer = _shootDelay;
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
		AddSibling(newBullet);
	}
}
