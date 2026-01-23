using Godot;
using System;

public partial class Menu : Node2D
{
	private Setting settingsScene;
	private Button settingsBtn;  

	public override void _Ready()
	{
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
	}

	public void OnSettingsBtnPressed()
	{
		Visible = false;
		settingsScene.Visible = true;
	}
	
}
