using System;
using System.Collections;
using Godot;

namespace MonsterTamerRoguelite.Scripts;

[GlobalClass]
public partial class Projectile : Area2D
{
    public Node2D Instigator;
    public Vector2 Direction = Vector2.Zero;
    public float Speed = 50.0f;
    public float Range = 100.0f;

    private Vector2 _startPos = Vector2.Zero;

    public delegate void OnExpirationEventHandler();
    public event OnExpirationEventHandler OnExpirationEvent = delegate{};

    public delegate void OnValidTargetHitHandler(Area2D target);
    public event OnValidTargetHitHandler OnValidTargetHit = delegate{};
    
    public override void _Ready()
    {
        base._Ready();

        _startPos = Instigator.Position;

        AreaEntered += OnAreaEntered;
    }

    private void OnAreaEntered(Area2D area)
    {
        if (area == Instigator || area.GetOwner() == Instigator || Instigator.IsAncestorOf(area))
        {
            return;
        }

        OnValidTargetHit(area);
        GD.Print($"{area.Name} hit on {area.Owner.Name} object!");
        QueueFree();
    }

    public override void _PhysicsProcess(double delta)
    {
        Position += Direction * Speed;

        Vector2 dist = Position - _startPos;
        
        if (dist.LengthSquared() >= Range * Range)
        {
            OnExpirationEvent();
            GD.Print("Projectile expired.");
            QueueFree();
        }
    }
}