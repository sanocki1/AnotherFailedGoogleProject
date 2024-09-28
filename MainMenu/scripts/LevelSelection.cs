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
		GetTree().ChangeSceneToFile("res://"); 
	}

	private void OnLevel2ButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://");
	}

	private void OnLevel3ButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://");
	}

	private void OnBackButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://MainMenu/scenes/main_menu.tscn");
	}
}
