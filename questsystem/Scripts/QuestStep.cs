using Godot;
using System;

[GlobalClass]
public abstract partial class QuestStep : Resource
{
    [Export] public string stepId { get; set; }
    [Export] public string description { get; set; }
    public bool completed { get; protected set; } = false;

    public virtual void Start(Node questManager) { }
    public virtual void CleanUp(Node questmanager) { }
    public virtual void OnEvent(string eventId, Variant data = default) { }
    public virtual void Update(double delta) { }

    public virtual void Complete()
    {
        if (completed) return;
        completed = true;
        EmitCompletion();
    }

    public delegate void StepCompletedEventHandler(QuestStep step);
    public event StepCompletedEventHandler StepCompleted;

    protected void EmitCompletion()
    {
        StepCompleted?.Invoke(this);
    }
}
