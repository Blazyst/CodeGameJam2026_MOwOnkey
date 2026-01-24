using Godot;
using System;

public partial class DeathMenu : Node2D
{
	private Button backMenuBtn;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		backMenuBtn = GetNode<Button>("BackMenuBtn");  
		if (backMenuBtn != null)
		{
			backMenuBtn.Pressed += OnBackMenuBtnBtnPressed;
		}
	}
	
	public void OnBackMenuBtnBtnPressed()
	{
		var scene = ResourceLoader.Load<PackedScene>("res://scenes/menu.tscn").Instantiate();
		GetTree().Root.AddChild(scene);
		GetTree().Root.GetNode<Node2D>("DeathMenu").QueueFree();
	}
}
