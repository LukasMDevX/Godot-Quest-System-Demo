using Godot;
using System;

public partial class DemoReviveEnemyButton : Button
{
    [Export]
    public Enemy targetEnemy;

    public override void _Ready()
    {
        Pressed += OnPressed;
    }

    private void OnPressed()
    {
        if (targetEnemy == null)
        {
            GD.PrintErr("No Enemy assigned!");
            return;
        }

        targetEnemy.Revive();
    }
}
