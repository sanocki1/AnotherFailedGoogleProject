using Godot;
using System;

public partial class YouLostScreen : Control
{
	public override void _Ready()
	{
		GetNode<Button>("VBoxContainer/PlayAgainButton").GrabFocus();
	}

	private void OnPlayAgainButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://Levels/scenes/level2.tscn");
	}

	private void OnMainMenuButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://MainMenu/scenes/main_menu.tscn");
	}
}
