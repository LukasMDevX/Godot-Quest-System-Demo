using Godot;
using System;

[GlobalClass]
public partial class DemoAreaTrigger : Node
{
    [Signal] public delegate void EnteredEventHandler();
    [Signal] public delegate void ExitedEventHandler();

    public void SimulateEnter()
    {
        GD.Print("Simulated area enter");
        EmitSignal(SignalName.Entered);
    }

    public void SimulateExit()
    {
        GD.Print("Simulated area exit");
        EmitSignal(SignalName.Exited);
    }
}
