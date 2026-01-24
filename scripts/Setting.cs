using Godot;
using System;

public partial class Setting : Node2D
{
	[Export] private OptionButton ResolutionOptionButton;

	public override void _Ready()
	{
		var backBtn = GetNode<Button>("BackBtn");
		if (backBtn != null)
		{
			backBtn.Pressed += OnBackPressed;
		}
		
		Window window = GetWindow();
    
		// Ajouter les options
		foreach (Vector2I res in Resolutions)
		{
			ResolutionOptionButton.AddItem($"{res.X}x{res.Y}");
		}
    
		// Sélectionner l'index correspondant à la résolution actuelle
		for (int i = 0; i < Resolutions.Length; i++)
		{
			if (Resolutions[i] == window.Size)
			{
				ResolutionOptionButton.Select(i);
				break;
			}
		}
    
		// Connecter le signal (via l'éditeur ou code)
		ResolutionOptionButton.ItemSelected += OnResolutionSelected;
	}

	public void OnBackPressed()
	{
		Visible = false;
		var menuParent = GetParent() as Menu;
		menuParent.Visible = true;  
	}
	
	private void OnResolutionSelected(long index)
	{
		Window window = GetWindow();
		window.Size = Resolutions[(int)index];
	}

	
	private Vector2I[] Resolutions = new Vector2I[]
	{
		new(800, 600),    // 4:3 classique, très léger
		new(1024, 768),   // 4:3 standard
		new(1280, 720),   // 16:9 HD, bon équilibre
		new(1366, 768),   // 16:10 laptop courant
		new(1600, 900),   // 16:9 intermédiaire
		new(1920, 1080)   // 16:9 Full HD (max)
	};


}
