using Godot;
using System;
using System.Collections.Generic;

public partial class Lemur : StaticBody2D
{
	private Dictionary<Guid, Enemy> _enemiesInRange = new Dictionary<Guid, Enemy>();
	private PackedScene _bullet = GD.Load<PackedScene>("res://Bullet/scenes/Bullet.tscn");
	private Texture2D _enabledLemurRangeTexture = GD.Load<Texture2D>("res://Lemur/assets/lemur_range.png");
	private Texture2D _disabledLemurRangeTexture = GD.Load<Texture2D>("res://Lemur/assets/lemur_range_red.png");
	private float _shootTimer = 0f;
	private float _shootDelay = 1f;
	private bool _isPlaced = false;
	private int _collisionsDetected = 0;
	private Sprite2D _lemurRangeVisual;

	public override void _Ready()
	{
		_lemurRangeVisual = GetNode<Sprite2D>("LemurRange/LemurRangeVisual");
	}

	public override void _Process(double delta)
	{
		if (!_isPlaced)
		{
			HandlePlacement();
		}
	}
	public override void _PhysicsProcess(double delta)
	{
		if (_isPlaced)
		{
			HandleShooting(delta);  //consider separating target selection and shooting into separate methods for clarity
		}

	}

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

	public void ShootBullet(Vector2 direction)
	{
		var newBullet = (Bullet)_bullet.Instantiate();
		newBullet.Position = GlobalPosition;
		newBullet.Direction = direction;
		AddSibling(newBullet);
	}

	private void HandlePlacement()
	{
		Position = GetGlobalMousePosition();

		if (!CanBePlaced())
		{
			_lemurRangeVisual.Texture = _disabledLemurRangeTexture;
		}
		else
		{
			_lemurRangeVisual.Texture = _enabledLemurRangeTexture;
		}

		if (Input.IsActionJustPressed("right_mouse_click"))
		{
			QueueFree();
		}

		if (CanBePlaced() && Input.IsActionJustPressed("left_mouse_click"))
		{
			PlaceLemur();
		}
	}

	private bool CanBePlaced()
	{
		return _collisionsDetected == 0 && !_isPlaced && IsWithinScreenBounds();
	}

		private void OnLemurHitboxBodyEntered(StaticBody2D body)
	{
		UpdateCollisionsDetected(1);
	}

	private void OnLemurHitboxBodyExited(StaticBody2D body)
	{
		UpdateCollisionsDetected(-1);
	}

	public void UpdateCollisionsDetected(int value)
	{
		_collisionsDetected += value;
	}

	private bool IsWithinScreenBounds()
{
    var viewportRect = GetViewport().GetVisibleRect();
	viewportRect.Size = new  Vector2(viewportRect.Size.X - 212, viewportRect.Size.Y);
	viewportRect = viewportRect.Grow(-20);
    return viewportRect.HasPoint(GlobalPosition);
}

	private void PlaceLemur()
	{
		_isPlaced = true;
		_lemurRangeVisual.Visible = false;
		SetCollisionLayerValue(6, true);
	}

	private void HandleShooting(double delta)
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
}

