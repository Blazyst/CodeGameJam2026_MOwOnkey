using Godot;
using System;
using CodeGameJam2026_MOwOnkey.scripts;

public partial class Main : Node2D
{
	[Export] public int Points = 125;

	private const int START_BATTERY_PRICE = 25;
	
	private int currentBatteryPrice = START_BATTERY_PRICE;

	private int pointsEarnedPerClick = 1;
	
	private const int START_UPGRADE_PRICE = 45;
	
	private int currentUpgradePrice = START_UPGRADE_PRICE;
	
	private AudioStreamPlayer2D soundPlayer;
	
	private PauseMenu pauseMenu;
	
	private Foxy foxy;

	private Button btn;

	private Timer timer;
	private AnimatedSprite2D BatteryBar;
	private const float MAX_BATTERY = 100f;
	
	public float CurrentBattery { get; private set; } = 100f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BatteryBar = GetNode<AnimatedSprite2D>("Node2D/AnimatedSprite2D");
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
		this.btn = GetNode<Node2D>("Node2D").GetNode<Button>("Button");
		this.btn.ButtonDown += OnButtonDown;
		GetNode<Node2D>("Node2D").GetNode<Button>("ButtonBuyBattery").ButtonDown += onBuyBatteryButtonDown;
		GetNode<Node2D>("Node2D").GetNode<Button>("ButtonUpgrade").ButtonDown += onButtonUpgradeButtonDown;
		soundPlayer = GetNode<Node2D>("Node2D").GetNode<Button>("Button").GetNode<AudioStreamPlayer2D>("nosePlayer");
		timer = new Timer();
		AddChild(timer);
		timer.WaitTime = 60;
		timer.Start();
		timer.Autostart = false;
		timer.Timeout += Win;
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
			GetNode<Node2D>("Node2D").GetNode<Button>("ButtonBuyBattery").Text = "Battery (" + currentBatteryPrice.ToString() + ")";
			RechargeBattery();
		}
	}
	public void RechargeBattery()
	{
		CurrentBattery = CurrentBattery + 0.2f*MAX_BATTERY;
		if (CurrentBattery > MAX_BATTERY) CurrentBattery = MAX_BATTERY;
	}
	
	private void onButtonUpgradeButtonDown()
	{
		if (Points >= currentUpgradePrice)
		{
			pointsEarnedPerClick += 2;
			Points  -= currentUpgradePrice;
			GetNode<Label>("Label").Text = Points.ToString();
			currentUpgradePrice = (int)(currentUpgradePrice * 2.2);
			GetNode<Node2D>("Node2D").GetNode<Button>("ButtonUpgrade").Text = "Upgrade (" + currentUpgradePrice.ToString() + ")";
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		GetNode<Node2D>("Node2D").GetNode<Label>("Label").Text = ""+(int)Math.Round(timer.TimeLeft, 0);
		// if (foxy == null)
		// {
		// 	var scene = ResourceLoader.Load<PackedScene>("res://scenes/foxy.tscn").Instantiate<Foxy>();
		// 	foxy = scene;
		// 	AddChild(foxy);
		// 	foxy.Position = new Vector2(-500, 0);
		// }

		if (CurrentBattery <= 0)
		{
			Lose();
		}
		
		DrainBattery(0.05f);
		if (CurrentBattery < 0) CurrentBattery = 0;
		if (CurrentBattery >= 0.80f * MAX_BATTERY)
		{
			BatteryBar.Play("full");
		}
		else if (CurrentBattery >= 0.60f * MAX_BATTERY)
		{
			BatteryBar.Play("80%");
		}
		else if (CurrentBattery >= 0.40f * MAX_BATTERY)
		{
			BatteryBar.Play("60%");
		}
		else if (CurrentBattery >= 0.20f * MAX_BATTERY)
		{
			BatteryBar.Play("40%");
		}
		else
		{
			BatteryBar.Play("20%");
		}
        
		if (CurrentBattery <= 0)
		{
			CurrentBattery = 0;
			BatteryBar.Play("empty");
            
		}
	}

	public void DrainBattery(float amount)
	{
		CurrentBattery -= amount;
		GD.Print(CurrentBattery);
	}

	private void Win()
	{
		timer.Stop();
		var scene = ResourceLoader.Load<PackedScene>("res://scenes/winMenu.tscn").Instantiate();
		GetTree().Root.AddChild(scene);
		GetTree().Root.GetNode<Node2D>("Node2D").QueueFree();
	}

	public void Lose()
	{
		timer.Stop();
		var scene = ResourceLoader.Load<PackedScene>("res://scenes/death_menu.tscn").Instantiate();
		GetTree().Root.AddChild(scene);
		GetTree().Root.GetNode<Node2D>("Node2D").QueueFree();
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("Pause"))
		{
			timer.Paused = !timer.Paused;
			pauseMenu.Visible = !pauseMenu.Visible;
		}
	}
	
}
