using Godot;
using System;

public partial class DemoInteractButton : Button
{
    [Export]
    public Interactable targetInteractable;

    public override void _Ready()
    {
        Pressed += OnPressed;
    }

    private void OnPressed()
    {
        if (targetInteractable == null)
        {
            GD.PrintErr("No Interactable assigned!");
            return;
        }

        targetInteractable.Interact();
    }
}
