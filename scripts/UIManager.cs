using Godot;

namespace CodeGameJam2026_MOwOnkey.scripts;

/// <summary>
/// Manages user interface elements related to the flashlight mechanic, including updating the battery progress bar.
/// </summary>
/// <remarks>UIManager coordinates the display of battery status by linking a ProgressBar control to the current
/// battery value from the FlashlightMechanic. Ensure that both the BatteryBar and FlashlightScript references are
/// assigned for correct operation.</remarks>
public partial class UIManager : CanvasLayer
{
    [Export] public AnimatedSprite2D BatteryBar;
    [Export] public CodeGameJam2026_MOwOnkey.scripts.FlashLightMechanics FlashlightScript; // Le lien vers l'autre script

    public override void _Process(double delta)
    {
        // On vérifie que le lien est bien fait pour éviter les crashs
        if (FlashlightScript != null && BatteryBar != null)
        {
            float CurrentBattery = FlashlightScript.CurrentBattery;
            float MaxBattery = 100f;
            if (CurrentBattery < 0) CurrentBattery = 0;
            if (CurrentBattery >= 0.80f * MaxBattery)
            {
                BatteryBar.Play("full");
            }
            else if (CurrentBattery >= 0.60f * MaxBattery)
            {
                BatteryBar.Play("60%");
            }
            else if (CurrentBattery >= 0.40f * MaxBattery)
            {
                BatteryBar.Play("40%");
            }
            else if (CurrentBattery >= 0.20f * MaxBattery)
            {
                BatteryBar.Play("20%");
            }
            else
            {
                BatteryBar.Play("empty");
            }
        }
    }
}