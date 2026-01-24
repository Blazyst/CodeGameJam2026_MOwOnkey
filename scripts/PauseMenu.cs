using Godot;
using System;

public partial class PauseMenu : Node2D
{
	private Setting settingsScene;
	private Rule ruleScene;
	private Button settingsBtn;
	private Button ruleBtn;
	private Button backMenuBtn;

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

		var scene2 = GD.Load<PackedScene>("res://scenes/setting.tscn");
		var instance2 = scene.Instantiate();
		AddChild(instance);
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
		
		ruleBtn = GetNode<Button>("RuleBtn");  
		if (ruleBtn != null)
		{
			ruleBtn.Pressed += OnRuleBtnPressed;
		}
	}

	public void OnSettingsBtnPressed()
	{
		settingsScene.Visible = true;
	}
	
	public void OnBackMenuBtnBtnPressed()
	{
		var scene = ResourceLoader.Load<PackedScene>("res://scenes/menu.tscn").Instantiate();
		GetTree().Root.AddChild(scene);
		GetTree().Root.GetNode<Node2D>("MenuPause").QueueFree();
	}
	
	public void OnRuleBtnPressed()
	{
		ruleScene.Visible = true;
	}
}
