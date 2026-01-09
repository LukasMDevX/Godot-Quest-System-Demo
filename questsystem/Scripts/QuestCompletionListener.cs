using Godot;
using System;

public partial class QuestCompletionListener : Node
{
    public override void _Ready()
    {
        GameEvents.Instance.QuestCompleted += OnQuestCompleted;
    }

    private void OnQuestCompleted(string questId)
    {
        GD.Print($"[Demo] Quest {questId} completed – persistence hook");
    }
}
