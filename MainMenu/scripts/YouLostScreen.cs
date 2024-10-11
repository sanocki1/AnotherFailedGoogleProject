using Godot;
using System;

public partial class YouLostScreen : Control
{
	public string currentLevel;
	public override void _Ready()
	{
		var global = (Global)GetNode("/root/Global");
		currentLevel = global.currentLevelPath;
		GetNode<Button>("VBoxContainer/PlayAgainButton").GrabFocus();
	}

	private void OnPlayAgainButtonPressed()
	{
		GetTree().ChangeSceneToFile(currentLevel);
	}

	private void OnMainMenuButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://MainMenu/scenes/main_menu.tscn");
	}
}
