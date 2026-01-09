using Godot;
using System;

[GlobalClass]
public partial class InteractStep : QuestStep
{
    [Export] public NodePath targetNode { get; set; }
    [Export] public NodePath signalSource;
    [Export] public string eventName = "Interacted";
    private Node qM;
    public override void Start(Node questManager)
    {
        qM = questManager;
        Node target = qM.GetNodeOrNull<Node>(targetNode);
        Node signalSourceNode = qM.GetNodeOrNull<Node>(signalSource);
        if (target == null)
        {
            GD.PrintErr($"[InteractStep] Target node not found: {targetNode}");
            return;
        }
        if (signalSource == null)
        {
            GD.PrintErr("InteractStep: No signal source assigned!");
            return;
        }
        if (!signalSourceNode.HasSignal(eventName))
        {
            GD.PrintErr($"Signal '{eventName}' not found on {signalSourceNode.Name}");
            return;
        }

        signalSourceNode.Connect(eventName, Callable.From(() => Complete()) );

        // Enable interactable when this step begins
        Interactable interactable = target.GetNodeOrNull<Interactable>("Interactable") as Interactable;
        if(interactable != null ) {  interactable.SetEnabled(true); }
    }


    public override void Complete()
    {
        base.Complete();

        var target = qM.GetNodeOrNull<Node>(targetNode);
        if (target is Interactable interactable)
            interactable.SetEnabled(false);
    }
}
