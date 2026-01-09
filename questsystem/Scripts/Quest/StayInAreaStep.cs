using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;

[GlobalClass]
public partial class StayInAreaStep : QuestStep
{
    [Export] public NodePath AreaTriggerPath { get; set; }
    private Node _areaTrigger;
    [Export] public float RequiredTime = 5f;

    [Export] public NodePath[] EffectPath { get; set; }
    private List<Node> toEffectNodes = new();
    [Export] public NodePath[] EffectOnFinishPaths { get; set; }
    private List<Node> effectOnFinishNodes = new();

    private float _timer = 0f;
    private bool _areaTriggered = false;

    public override void Start(Node questManager)
    {
        _areaTrigger = questManager.GetNode(AreaTriggerPath);
        _areaTrigger.Connect("Entered", Callable.From(OnEntered));
        _areaTrigger.Connect("Exited", Callable.From(OnExited));

        toEffectNodes.Clear();
        foreach (var path in EffectPath)
        {
            if (path != null)
            {
                Node n = questManager.GetNodeOrNull<Node>(path);
                if (n != null)
                {
                    toEffectNodes.Add(n);
                    n.Call("Start");
                }
            }
        }

        effectOnFinishNodes.Clear();
        foreach (var path in EffectOnFinishPaths)
        {
            if (path != null)
            {
                Node n = questManager.GetNodeOrNull<Node>(path);
                if (n != null)
                {
                    effectOnFinishNodes.Add(n);
                }
            }
        }
    }

    private void OnEntered()
    {
        _areaTriggered = true;
        GD.Print("Entered area (via trigger)");
    }

    private void OnExited()
    {
        _areaTriggered = false;
        GD.Print("Exited area (via trigger)");
    }
    private void OnBodyEntered(Node3D body)
    {
        if (body.Name == "Player") _areaTriggered = true;
        GD.Print("Player entered area");
    }

    private void OnBodyExited(Node3D body)
    {
        if (body.Name == "Player") _areaTriggered = false;
        GD.Print("Player left area");
    }

    public override void Update(double delta)
    {
        if (_areaTriggered)
        {
            _timer += (float)delta;
            float progress = _timer / RequiredTime;
            foreach (var n in toEffectNodes)
            {
                n.Call("Effect", progress);
            }
            if (_timer >= RequiredTime)
            {
                // Trigger finish effects
                foreach (var n in effectOnFinishNodes)
                {
                    n.Call("Effect");
                }
                Complete();
            }

        }
        else
        {
            _timer = 0f; // reset if player leaves early
        }
    }

    public override void CleanUp(Node questManager)
    {
        if (_areaTrigger != null)
        {
            _areaTrigger.Disconnect("Entered", Callable.From(OnEntered));
            _areaTrigger.Disconnect("Exited", Callable.From(OnExited));
        }
    }
}
