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
	
	private AudioStreamPlayer2D soundPlayer;
	
	private PauseMenu pauseMenu;

	private Button btn;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var scene = GD.Load<PackedScene>("res://scenes/pause_menu.tscn");
		var instance = scene.Instantiate() as Node2D;
		AddChild(instance);
		instance.Visible = false;
		pauseMenu = GetNode<Node2D>("PauseMenu") as PauseMenu;
		var test = GetTree();
		if (pauseMenu != null)
		{
			pauseMenu.Visible = false;
		}
		this.btn = GetNode<Button>("Button");
		this.btn.ButtonDown += OnButtonDown;
		GetNode<Button>("ButtonBuyBattery").ButtonDown += onBuyBatteryButtonDown;
		GetNode<Button>("ButtonUpgrade").ButtonDown += onButtonUpgradeButtonDown;
		soundPlayer = GetNode<AudioStreamPlayer2D>("nosePlayer");
	}

	private void OnButtonDown()
	{
		Points += pointsEarnedPerClick;
		GetNode<Label>("Label").Text = Points.ToString();
		soundPlayer.Play();
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

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("Pause"))
		{
			pauseMenu.Visible = !pauseMenu.Visible;
		}
	}
	
}
