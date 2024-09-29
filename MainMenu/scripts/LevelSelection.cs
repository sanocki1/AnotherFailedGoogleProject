using Godot;
using System;

public partial class LevelSelection : Control
{

	public override void _Ready()
	{
		GetNode<Button>("HFlowContainer/Level1Button").GrabFocus();
	}

	private void OnLevel1ButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://Levels/scenes/level2.tscn"); 
	}

	private void OnLevel2ButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://Levels/scenes/level2.tscn");
	}

	private void OnLevel3ButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://Levels/scenes/level2.tscn");
	}

	private void OnBackButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://MainMenu/scenes/main_menu.tscn");
	}
}
