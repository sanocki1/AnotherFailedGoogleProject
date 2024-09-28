using Godot;
using System;

public partial class Intro : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var timer = GetNode<Timer>("Timer");
		timer.WaitTime = 35;
		timer.OneShot = true;
		timer.Autostart = true;
		timer.Connect("timeout", new Callable(this, nameof(OnTimerTimeout)));
		timer.Start();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnTimerTimeout()
	{
		GetTree().ChangeSceneToFile("res://MainMenu/scenes/main_menu.tscn");
	}
}
