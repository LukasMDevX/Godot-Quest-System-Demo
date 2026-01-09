using Godot;
using System;

public partial class Enemy : Node3D
{ 

    public bool isDead = false;

    //Signals
    [Signal]
    public delegate void EnemyDiedEventHandler();

    public override void _Ready()
    {

    }

    public void Die()
    {
        if(isDead) return;
        isDead = true;

        GD.Print($"Enemy {Name} died!");

        GetTree().CurrentScene.GetNode<Node>("QuestManager").Call("HandleEvent", "EnemyKilled");

        // Emit event for quest or other listeners
        EmitSignal(SignalName.EnemyDied);
    }

    public void Revive()
    {
        if (!isDead) return;
        isDead = false;
        GD.Print($"Enemy {Name} got revived!");
    }
}
