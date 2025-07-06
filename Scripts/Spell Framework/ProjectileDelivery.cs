using Godot;
using System;
using MonsterTamerRoguelite.Scripts;

[GlobalClass]
public partial class ProjectileDelivery : Resource, IDeliveryStrategy
{
    [Export] public PackedScene ProjectileScene;
    [Export] public float Speed = 1.0f;

    public void Execute(TargetData data, Action<TargetData> onDeliveryCallback)
    {
        if (ProjectileScene == null || data.Direction == null) return;

        if (data.SourceUnit == null) return;

        var proj = ProjectileScene.Instantiate<Projectile>();
        proj.GlobalPosition = data.SourceUnit.GlobalPosition;
        
        Vector2 dir = data.Direction;
        proj.Direction = dir.Normalized();
        proj.Speed = Speed;
        proj.Instigator = data.SourceUnit;
        
        proj.OnValidTargetHit += (area) =>
        {
            onDeliveryCallback(data);
        };

        proj.OnExpirationEvent += () =>
        {
            onDeliveryCallback(data);
        };

        data.SourceUnit.GetTree().CurrentScene.AddChild(proj);
    }
}
