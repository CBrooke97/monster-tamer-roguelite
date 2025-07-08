using Godot;
using System;
using System.ComponentModel.Design;
using MonsterTamerRoguelite.Scripts;

public sealed class TargetData
{
    public Vector2I? OriginCell { get; set; } = Vector2I.Zero;
    public Vector2I Direction { get; set; } = Vector2I.Zero; 
    public MTRCharacter? TargetUnit { get; set; }
    public MTRCharacter? SourceUnit { get; set; }
}

public interface ITargetingStrategy
{
    /// <summary>
    /// Called by the UI layer. Implement live highlighting & validation here.
    /// When the player confirms their choice, invoke onConfirm with the filled TargetData.
    /// If the player cancels, invoke onCancel.
    /// </summary>
    void BeginSelection(TargetData data, Action<TargetData> onConfirmCallback, Action onCancelCallback);

    void Tick();
}

public interface IDeliveryStrategy
{
    /// <summary>
    /// Spawn projectiles, beams, zones, etc. You may enqueue commands or call
    /// back into the combat system to keep long‑running effects deterministic.
    /// </summary>
    void Execute(TargetData data, Action<TargetData> onDeliveryCallback);
}

public interface IEffectStrategy
{
    /// <summary>
    /// Apply gameplay consequences such as damage, healing, statuses.
    /// Runs after (or during) delivery, depending on the spell definition.
    /// </summary>
    void Apply(TargetData data);
}

[GlobalClass]
public partial class SpellDefinition : Resource
{
    [Export] public string SpellName = "New Spell";
    [Export(PropertyHint.MultilineText)] public string Description;
    [Export] public Texture2D Icon;
    [Export] public int ActionPointCost = 1;
    [Export] public float CooldownTurns = 0;

    // Wire up the three orthogonal behaviours. Each should be another Resource
    // that implements the corresponding interface.
    [Export]
    private Resource _targetingStrategy;
    
    public ITargetingStrategy TargetingStrategy
    {
        get => _targetingStrategy as ITargetingStrategy;
        set
        {
            if (value is not Resource resource)
                throw new ArgumentException($"{nameof(value)} must implement {nameof(ITargetingStrategy)}.", nameof(value));

            _targetingStrategy = resource;
        }
    }
        
    [Export] private Resource _deliveryStrategy;
    
    public IDeliveryStrategy DeliveryStrategy
    {
        get => _deliveryStrategy as IDeliveryStrategy;
        set
        {
            if (value is not Resource resource)
                throw new ArgumentException($"{nameof(value)} must implement {nameof(IDeliveryStrategy)}.", nameof(value));

            _deliveryStrategy = resource;
        }
    }
    
    [Export] private Resource _effectStrategy;
    
    public IEffectStrategy EffectStrategy
    {
        get => _effectStrategy as IEffectStrategy;
        set
        {
            if (value is not Resource resource)
                throw new ArgumentException($"{nameof(value)} must implement {nameof(IEffectStrategy)}.", nameof(value));

            _effectStrategy = resource;
        }
    }
}
