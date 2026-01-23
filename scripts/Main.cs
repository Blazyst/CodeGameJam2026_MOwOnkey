using Godot;
using System;

public partial class Main : Node2D
{
	private Timer timer;
	
	private PauseMenu pauseMenu;

	private Button btn;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var scene = GD.Load<PackedScene>("res://scenes/pause_menu.tscn");
		var instance = scene.Instantiate();
		AddChild(instance);
		pauseMenu = GetNode<Node2D>("PauseMenu") as PauseMenu;  
		
		if (pauseMenu != null)
		{
			pauseMenu.Visible = false;
		}
		
		this.timer = new Timer();
		AddChild(timer);
		timer.WaitTime = 150;
		timer.Start();
		this.btn = GetNode<Button>("Button");
		this.btn.ButtonDown += OnButtonDown;
	}

	private void OnButtonDown()
	{
		double time = timer.TimeLeft;
		timer.Stop();
		timer.WaitTime = time += 1.0;
		timer.Start();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		GetNode<Label>("Label").Text = timer.TimeLeft.ToString("F0");
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("Pause"))
		{
			pauseMenu.Visible = !pauseMenu.Visible;
		}
	}
	
}
