using Godot;
using System;


/// <summary>
/// Provides flashlight functionality for a 2D game character, including battery management, light scaling, and
/// interaction with target groups.
/// </summary>
/// <remarks>This class manages the visual and interactive aspects of a flashlight mechanic, such as draining
/// battery power while the flashlight is active, adjusting the light's scale based on remaining battery, and affecting
/// objects in a specified group when illuminated. The flashlight's state and behavior are updated each frame. Attach
/// this component to a Node2D in your scene to enable flashlight controls and interactions.</remarks>
public partial class FlashlightMechanic : Node2D
{
	[ExportGroup("Composants")]
	[Export] public PointLight2D LightVisual;
	[Export] public Area2D Hitbox;
	[Export] public CollisionShape2D HitboxShape;

	[ExportGroup("Batterie")]
	[Export] public float MaxBattery = 100.0f;
	[Export] public float DrainRate = 15.0f;

	[ExportGroup("Lumière")]
	[Export] public float MaxScale = 1.0f;
	[Export] public float MinScale = 0.3f;
	[Export] public string TargetGroupName = "FoxyGroup";

	public float CurrentBattery { get; private set; }
	private bool _isLightActive = false;

	public override void _Ready()
	{
		CurrentBattery = MaxBattery;
		UpdateLightState(false);
	}

	public override void _Process(double delta)
	{
		GlobalPosition = GetGlobalMousePosition();
		if (Input.IsActionPressed("use_flashlight") && CurrentBattery > 0)
		{
			_isLightActive = true;
			HandleBattery((float)delta);
			CheckCollisions();
		}
		else
		{
			_isLightActive = false;
		}
		UpdateLightState(_isLightActive);
	}

	private void HandleBattery(float delta)
	{
		CurrentBattery -= DrainRate * delta;
		if (CurrentBattery < 0) CurrentBattery = 0;
		float batteryPercent = CurrentBattery / MaxBattery;
		float currentScale = MaxScale;

		if (batteryPercent < 0.5f)
		{
			float t = batteryPercent / 0.5f;
			currentScale = Mathf.Lerp(MinScale, MaxScale, t);
		}
		Vector2 newScale = new Vector2(currentScale, currentScale);
		LightVisual.TextureScale = currentScale;
		Hitbox.Scale = newScale;
	}

	private void CheckCollisions()
	{
		var overlappingBodies = Hitbox.GetOverlappingBodies();

		foreach (var body in overlappingBodies)
		{
			if (body.IsInGroup(TargetGroupName))
			{
				body.QueueFree();
			}
		}
	}

	private void UpdateLightState(bool isOn)
	{
		LightVisual.Enabled = isOn;
		Hitbox.Monitoring = isOn;
	}
}
