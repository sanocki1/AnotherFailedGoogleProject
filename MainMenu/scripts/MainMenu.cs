using Godot;
using System;

public partial class MainMenu : Control
{

	public override void _Ready()
	{
		GetNode<Button>("Panel/VBoxContainer/PlayButton").GrabFocus();
	}
	private void OnPlayButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://MainMenu/scenes/level_selection.tscn");
	}

	private void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}
}
