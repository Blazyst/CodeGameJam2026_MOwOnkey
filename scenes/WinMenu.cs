using Godot;
using System;

public partial class WinMenu : Node2D
{
    private Button backMenuBtn;
	
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        AudioStreamPlayer2D winSound = GetNode<AudioStreamPlayer2D>("WinSound");
        winSound.Play();
        winSound.Finished += () =>
        {
            var scene = ResourceLoader.Load<PackedScene>("res://scenes/menu.tscn").Instantiate();
            GetTree().Root.AddChild(scene);
            GetTree().Root.GetNode<Node2D>("winMenu").QueueFree();
        };
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        
    }
}