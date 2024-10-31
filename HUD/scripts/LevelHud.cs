using Godot;
using System;

public partial class LevelHud : Control
{
	[Signal] public delegate void PlayerLostEventHandler();
	[Signal] public delegate void LemurButtonPressedEventHandler();
	private int _health = 100;
	private float _baseHealthWidth;
	private ColorRect _healthForeground;
	private int _coinAmount = 300;
	private Label _coinAmountLabel;

	public override void _Ready()
	{
		_healthForeground = GetNode<ColorRect>("Healthbar/HealthForeground");
		_baseHealthWidth = _healthForeground.Size.X;
		UpdateHealthText();
		UpdateHealthForeground();

		_coinAmountLabel = GetNode<Label>("Vault/VaultLabel");
		_coinAmount = 300;
		UpdateVaultText();
	}
	private void UpdateHealthText()
	{
		Label current_health = GetNode<Label>("Healthbar/HealthAmount");
		current_health.Text = _health.ToString() + "%";
	}

	private void UpdateHealthForeground()
	{
		_healthForeground.Size = new Vector2(_baseHealthWidth * (_health / 100.0f), _healthForeground.Size.Y);
	}

	public void ApplyDamage(int damage)
	{
		_health -= damage;
		_health = Mathf.Clamp(_health, 0, 100);
		UpdateHealthText();
		UpdateHealthForeground();
		if (_health == 0)
		{
			EmitSignal(SignalName.PlayerLost);
		}
	}

	private void UpdateVaultText()
	{
		_coinAmountLabel.Text = _coinAmount + "$";
	}

	public void AddCoins(int coins)
	{
		_coinAmount += coins;
		UpdateVaultText();
	}

	public Boolean SpendCoins(int coins)
	{
		/*
		* Tries to spend coins. If enough coins are available, it returns true and deducts coins.
		* If there are not enough coins, it returns false and does not change the coin amount.
		*/
		if (_coinAmount >= coins)
		{
			_coinAmount -= coins;
			UpdateVaultText();
			return true;
		}
		return false;
	}

	private void OnSelectLemur1Pressed()
	{
		EmitSignal(SignalName.LemurButtonPressed);
	}
}
