using Godot;
using System;

public partial class DemoAreaTriggerExitButton : Button
{
    [Export]
    public DemoAreaTrigger areaTrigger;

    public override void _Ready()
    {
        Pressed += OnPressed;
    }

    private void OnPressed()
    {
        if (areaTrigger == null)
        {
            GD.PrintErr("No Area assigned!");
            return;
        }

        areaTrigger.SimulateExit();
    }
}
