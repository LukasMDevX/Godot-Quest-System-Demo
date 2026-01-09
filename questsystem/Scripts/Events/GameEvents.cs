using Godot;
using System;


public partial class GameEvents : Node
{
    private static GameEvents instance;

    [Signal]
    public delegate void QuestCompletedEventHandler(string questId);

    public static GameEvents Instance
    {
        get { return instance; }
    }

    public override void _Ready()
    {
        instance = this;
    }
}
