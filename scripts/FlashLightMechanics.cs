using Godot;

namespace CodeGameJam2026_MOwOnkey.scripts;

/// <summary>
/// Provides flashlight functionality for a 2D game character, including battery management, light scaling, and
/// interaction with target groups.
/// </summary>
/// <remarks>This class manages the visual and interactive aspects of a flashlight mechanic, such as draining
/// battery power while the flashlight is active, adjusting the light's scale based on remaining battery, and affecting
/// objects in a specified group when illuminated. The flashlight's state and behavior are updated each frame. Attach
/// this component to a Node2D in your scene to enable flashlight controls and interactions.</remarks>
public partial class FlashLightMechanics : Node2D
{
    private PointLight2D _light;
    private Area2D _area;

    public override void _Ready()
    {
        _light = GetNode<PointLight2D>("LumiereSouris");
        _area = GetNode<Area2D>("Area2D");
        ToggleFlashlight(false);
        _area.BodyEntered += OnBodyEntered;
        _area.BodyExited += OnBodyExited;
    }

    public override void _Process(double delta)
    {
        GlobalPosition = GetGlobalMousePosition();
        if (Input.IsMouseButtonPressed(MouseButton.Right))
        {
            ToggleFlashlight(true);
        }
        else
        {
            ToggleFlashlight(false);
        }
    }

    private void ToggleFlashlight(bool isActive)
    {
        _light.Enabled = isActive;

        _area.Monitoring = isActive;
        _area.Monitorable = isActive;

    }

    private void OnBodyEntered(Node2D body)
    {
        if (body.IsInGroup("Foxy-G"))
        {
            GD.Print($"J'ai trouvé un Foxy-G : {body.Name} !");
        }
    }

    private void OnBodyExited(Node2D body)
    {
        if (body.IsInGroup("Foxy-G"))
        {
            GD.Print($"{body.Name} n'est plus dans la lumière.");
        }
    }

    public float CurrentBattery { get; private set; } = 100f;

    public void DrainBattery(float amount)
    {
        CurrentBattery -= amount;
        if (CurrentBattery < 0) CurrentBattery = 0;
    }
}