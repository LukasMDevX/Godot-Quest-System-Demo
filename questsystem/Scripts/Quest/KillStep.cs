using Godot;
using System;

[GlobalClass]
public partial class KillStep : QuestStep
{
    [Export] public int targetCount = 10;
    private int _killCount = 0;

    public override void OnEvent(string eventId, Variant data)
    {
        if (eventId != "EnemyKilled") return;

        _killCount++;
        if (_killCount >= targetCount)
            Complete();
    }
}
