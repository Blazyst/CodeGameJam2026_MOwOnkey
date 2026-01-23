using Godot;
using System;

public partial class Setting : Node2D
{
	public override void _Ready()
	{
		var backBtn = GetNode<Button>("BackBtn");
		if (backBtn != null)
		{
			backBtn.Pressed += OnBackPressed;
		}
	}

	public void OnBackPressed()
	{
		Visible = false;
		var menuParent = GetParent() as Menu;
		menuParent.Visible = true;  
	}
}
