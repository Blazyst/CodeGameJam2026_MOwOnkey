using Godot;
using System;

public partial class Main : Node2D
{
	[Export] public int Points = 25;

	private const int START_BATTERY_PRICE = 25;
	
	private int currentBatteryPrice = START_BATTERY_PRICE;

	private int pointsEarnedPerClick = 1;
	
	
	private const int START_UPGRADE_PRICE = 75;
	
	private int currentUpgradePrice = START_UPGRADE_PRICE;

	private Button btn;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.btn = GetNode<Button>("Button");
		this.btn.ButtonDown += OnButtonDown;
		GetNode<Button>("ButtonBuyBattery").ButtonDown += onBuyBatteryButtonDown;
		GetNode<Button>("ButtonUpgrade").ButtonDown += onButtonUpgradeButtonDown;
	}

	private void OnButtonDown()
	{
		Points += pointsEarnedPerClick;
		GetNode<Label>("Label").Text = Points.ToString();
	}

	private void onBuyBatteryButtonDown()
	{
		// battery goes up by 20%
		if (Points >= currentBatteryPrice)
		{
			Points -= currentBatteryPrice;
			GetNode<Label>("Label").Text = Points.ToString();
			currentBatteryPrice = (int)(currentBatteryPrice * 1.2);
			GetNode<Button>("ButtonBuyBattery").Text = "Battery (" + currentBatteryPrice.ToString() + ")";
		}
	}
	
	private void onButtonUpgradeButtonDown()
	{
		if (Points >= currentUpgradePrice)
		{
			pointsEarnedPerClick += 2;
			Points  -= currentUpgradePrice;
			GetNode<Label>("Label").Text = Points.ToString();
            currentUpgradePrice = (int)(currentUpgradePrice * 2.2);
            GetNode<Button>("ButtonUpgrade").Text = "Upgrade (" + currentUpgradePrice.ToString() + ")";
		}
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
