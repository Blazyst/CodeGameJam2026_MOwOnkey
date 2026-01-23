using Godot;
using System;

/// <summary>
/// Controls the color modulation of the canvas to simulate a day-night cycle effect, smoothly transitioning between
/// specified day and night colors over a configurable duration.
/// </summary>
/// <remarks>Use this class to visually represent time-of-day changes in a scene by adjusting the canvas color.
/// The cycle duration and start time can be customized, as well as the colors used for day and night. The color
/// transitions automatically as the cycle progresses.</remarks>
public partial class DayNightCycle : CanvasModulate
{
	[ExportGroup("Cycle")]
	[Export] public float DayDurationSeconds = 60.0f;
	[Export] public bool StartAtNight = true; 

	[ExportGroup("Couleurs")]
	[Export] public Color DayColor = new Color(1, 1, 1, 1);
	[Export] public Color NightColor = new Color(0.09f, 0.09f, 0.15f, 1);

	private double _time;

	public override void _Ready()
	{
		if (StartAtNight)
		{
			_time = DayDurationSeconds * 0.75f;
		}

		_Process(0);
	}

	public override void _Process(double delta)
	{
		_time += delta;
		float sinVal = Mathf.Sin((float)(_time * Math.PI * 2 / DayDurationSeconds));
		float lerpVal = (sinVal + 1.0f) / 2.0f;
		this.Color = NightColor.Lerp(DayColor, lerpVal);
	}
}
