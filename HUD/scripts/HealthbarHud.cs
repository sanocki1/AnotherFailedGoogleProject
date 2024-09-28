using Godot;
using System;
using System.Drawing;
using System.Runtime.CompilerServices;

public partial class HealthbarHud : Node2D
{
	private int health;

	private void update_text()
	{
		Label current_health = this.GetNode<Label>("health_ammount");
    	current_health.Text = this.health.ToString() + "%";
	}

	private void update_foreground()
	{
		ColorRect health_foreground = this.GetNode<ColorRect>("health_foreground");
		health_foreground.Size = new Vector2(health_foreground.Size.X * (health / 100.0f), health_foreground.Size.Y);
	}

	private void apply_damage(int damage_count)
	{
		this.health -= damage_count;
		this.health = Math.Max(this.health, 0);
	}

	public Boolean is_alive()
	{
		return this.health > 0;
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.health = 100;
		this.update_text();
		this.update_foreground();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		this.update_text();
		this.update_foreground();
	}
}
