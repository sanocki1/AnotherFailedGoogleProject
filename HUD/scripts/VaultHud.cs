using Godot;
using System;

public partial class VaultHud : Node2D
{
	private int coins;
	// Called when the node enters the scene tree for the first time.

	private void update_text()
	{
		Label coin_ammount = this.GetNode<Label>("coin_ammount");
    	coin_ammount.Text = "Coins: " + this.coins.ToString();
	}
	
	public void add_coins(int coins)
	{
		this.coins += coins;
	}

	public Boolean spend_coins(int coins)
	{
		/*
		* Tries to spend coins. If enough coins are available, it returns true and deducts coins.
		* If there are not enough coins, it returns false and does not change the coin amount.
		*/
		 if (this.coins >= coins)
		{
			this.coins -= coins; 
			return true;
		}
		return false;
		
	}
	public override void _Ready()
	{
		this.coins = 300;
		update_text();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		update_text();
	}
}
