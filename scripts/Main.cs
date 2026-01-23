using Godot;
using System;

public partial class Main : Node2D
{
	private Timer timer;

	private Button btn;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
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
}
