using Godot;
using System;

public partial class VaultHud : Node2D
{
	private int _coins;
	private Label _coinAmountLabel;

	public override void _Ready()
	{
		_coinAmountLabel = GetNode<Label>("CoinAmountLabel");
		_coins = 300;
		UpdateText();
	}

	private void UpdateText()
	{
		_coinAmountLabel.Text = _coins + "$";
	}

	public void AddCoins(int coins)
	{
		_coins += coins;
		UpdateText();
	}

	public Boolean SpendCoins(int coins)
	{
		/*
		* Tries to spend coins. If enough coins are available, it returns true and deducts coins.
		* If there are not enough coins, it returns false and does not change the coin amount.
		*/
		if (_coins >= coins)
		{
			_coins -= coins;
			UpdateText();
			return true;
		}
		return false;
	}
}
