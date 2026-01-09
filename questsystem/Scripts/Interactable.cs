using Godot;
using System;
using System.Reflection;

public partial class Interactable : Node
{
    [Export]
    public Node[] effectObjects;

    private Node3D parentNode;

    private bool _isActivated = false;
    private bool _isEnabled = false;

    [Signal]
    public delegate void InteractedEventHandler();

    public override void _Ready()
    {
        parentNode = (Node3D)GetParent();
    }

    public void SetEnabled(bool enabled)
    {
        _isEnabled = enabled;
    }

    public void Interact()
    {
        if (!_isEnabled)
        {
            GD.Print($"{Name}: Ignored interaction — not active quest step.");
            return;
        }

        // if only interactable once
        if (_isActivated)
            return;

        _isActivated = true;

        GD.Print($"Interactable {parentNode.Name} was interacted with!");

        // Play animation and stuff... whatever should happen
        foreach (Node node in effectObjects)
        {
            node.Call("Effect");
        }

        // Emit event for quest or other listeners
        EmitSignal(SignalName.Interacted);
    }

    public void Reset()
    {
        _isActivated = false;
    }
}
