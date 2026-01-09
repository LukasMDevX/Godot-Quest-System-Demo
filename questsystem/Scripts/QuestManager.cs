using Godot;
using System;

public partial class QuestManager : Node
{
    [Export] public Godot.Collections.Array<QuestStep> Steps { get; set; }

    private int _currentStepIndex = 0;
    private QuestStep CurrentStep => _currentStepIndex < Steps.Count ? Steps[_currentStepIndex] : null;

    [Signal]
    public delegate void QuestCompletedEventHandler(string questId);
    private string _questId;

    public override void _Ready()
    {
        if (Steps.Count == 0) return;

        foreach (var step in Steps)
            step.StepCompleted += OnStepCompleted;

        CurrentStep?.Start(this);
    }

    public override void _Process(double delta)
    {
        CurrentStep?.Update(delta);
    }

    public void HandleEvent(string eventId, Variant data = default)
    {
        CurrentStep?.OnEvent(eventId, data);
    }
    public void HandleEvent(string eventId)
    {
        HandleEvent(eventId, default);
    }

    private void OnStepCompleted(QuestStep step)
    {
        GD.Print($"[QuestManager] Step completed: {step.stepId}");
        CurrentStep?.CleanUp(this);
        _currentStepIndex++;

        if (_currentStepIndex < Steps.Count)
            Steps[_currentStepIndex].Start(this);
        else
            CompleteQuest();
    }

    private void CompleteQuest()
    {
        GD.Print("[QuestManager] Quest completed!");
        GameEvents.Instance.EmitSignal(GameEvents.SignalName.QuestCompleted, _questId);
    }
}
