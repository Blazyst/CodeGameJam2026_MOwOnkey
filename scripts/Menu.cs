using Godot;
using System;

public partial class Menu : Node2D
{
	private Setting settingsScene;
	private Rule ruleScene;
	private Button settingsBtn;
	private Button startBtn;
	private Button ruleBtn;
	private Button quitBtn;

	public override void _Ready()
	{
		var scene = GD.Load<PackedScene>("res://scenes/rule.tscn");
		var instance = scene.Instantiate();
		AddChild(instance);
		ruleScene = GetNode<Node2D>("Rule") as Rule; 
		if (ruleScene != null)
		{
			ruleScene.Visible = false;
		}
		
		settingsScene = GetNode<Node2D>("Setting") as Setting;  
		
		if (settingsScene != null)
		{
			settingsScene.Visible = false;
		}

		settingsBtn = GetNode<Button>("SettingsBtn");  
		if (settingsBtn != null)
		{
			settingsBtn.Pressed += OnSettingsBtnPressed;
		}
		
		startBtn = GetNode<Button>("StartBtn");  
		if (startBtn != null)
		{
			startBtn.Pressed += OnStartBtnPressed;
		}
		
		ruleBtn = GetNode<Button>("RuleBtn");  
		if (ruleBtn != null)
		{
			ruleBtn.Pressed += OnRuleBtnPressed;
		}
		
		quitBtn = GetNode<Button>("QuitBtn");
		if (quitBtn != null)
		{
			quitBtn.Pressed += OnQuitBtnPressed;
		}
	}

	public void OnSettingsBtnPressed()
	{
		settingsScene.Visible = true;
	}
	
	public void OnStartBtnPressed()
	{
		var scene = ResourceLoader.Load<PackedScene>("res://scenes/main.tscn").Instantiate();
		GetTree().Root.AddChild(scene);
		GetTree().Root.GetNode<Node2D>("Menu").QueueFree();
	}
	
	public void OnRuleBtnPressed()
	{
		ruleScene.Visible = true;
	}

	public void OnQuitBtnPressed()
	{
		GetTree().Quit();	
	}
	
}
