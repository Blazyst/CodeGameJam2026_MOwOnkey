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
    private const float MAX_BATTERY = 100f;
    [Export] public AnimatedSprite2D BatteryBar;

    public override void _Ready()
    {
        _light = GetNode<PointLight2D>("LumiereSouris");
        _area = GetNode<Area2D>("Area2D");
        ToggleFlashlight(true);
    }
    
    public void RechargeBattery()
    {
        CurrentBattery = CurrentBattery + 0.2f*MAX_BATTERY;
        if (CurrentBattery > MAX_BATTERY) CurrentBattery = MAX_BATTERY;
    }

    public override void _Process(double delta)
    {
        // GlobalPosition = GetGlobalMousePosition();
        // if (Input.IsMouseButtonPressed(MouseButton.Right))
        // {
        //     ToggleFlashlight(true);
        // }
        // else
        // {
        //     ToggleFlashlight(false);
        // }
        DrainBattery(0.05f);
        if (CurrentBattery < 0) CurrentBattery = 0;
        if (CurrentBattery >= 0.80f * MAX_BATTERY)
        {
            BatteryBar.Play("full");
        }
        else if (CurrentBattery >= 0.60f * MAX_BATTERY)
        {
            BatteryBar.Play("80%");
        }
        else if (CurrentBattery >= 0.40f * MAX_BATTERY)
        {
            BatteryBar.Play("60%");
        }
        else if (CurrentBattery >= 0.20f * MAX_BATTERY)
        {
            BatteryBar.Play("40%");
        }
        else
        {
            BatteryBar.Play("20%");
        }
        
        if (CurrentBattery <= 0)
        {
            CurrentBattery = 0;
            BatteryBar.Play("empty");
            
        }
    }

    private void ToggleFlashlight(bool isActive)
    {
        _light.Enabled = isActive;

        _area.Monitoring = isActive;
        _area.Monitorable = isActive;

    }

    public float CurrentBattery { get; private set; } = 100f;

    public void DrainBattery(float amount)
    {
        CurrentBattery -= amount;
        GD.Print(CurrentBattery);
    }
}