using Godot;
using System;
using System.Drawing;
using System.Runtime.CompilerServices;

public partial class HealthbarHud : Node2D
{
	private int _health;
	private float _baseHealthWidth;
	private ColorRect _healthForeground;

	private void UpdateText()
	{
		Label current_health = GetNode<Label>("health_ammount");
		current_health.Text = _health.ToString() + "%";
	}

	private void UpdateForeground()
	{

		_healthForeground.Size = new Vector2(_baseHealthWidth * (_health / 100.0f), _healthForeground.Size.Y);
	}

	public void ApplyDamage(int damage)
	{
		_health -= damage;
		_health = Mathf.Clamp(_health, 0, 100);
		UpdateText();
		UpdateForeground();
		if (_health == 0)
		{
			GetTree().ChangeSceneToFile("res://MainMenu/scenes/you_lost_screen.tscn");
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_health = 100;
		_healthForeground = GetNode<ColorRect>("health_foreground");
		_baseHealthWidth = _healthForeground.Size.X;
		UpdateText();
		UpdateForeground();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
