using Godot;

namespace MonsterTamerRoguelite.Scripts;

[GlobalClass]
public partial class HealthBarUI : TextureProgressBar
{
    private MTRCharacter? _target;

    public MTRCharacter? Target
    {
        get => _target;
        set
        {
            HealthComponent? oldHealthComp = _target?.GetNodeOrNull<HealthComponent>("HealthComponent");
            
            if (oldHealthComp != null)
            {
                oldHealthComp.OnHealthChanged -= OnHealthChanged;
            }
            
            HealthComponent? newHealthComp = value?.GetNodeOrNull<HealthComponent>("HealthComponent");
                
            if (newHealthComp != null)
            {
                newHealthComp.OnHealthChanged += OnHealthChanged;
                newHealthComp.OnMaxHealthChanged += OnMaxHealthChanged;
                Value = newHealthComp.CurrentHealth;
                MaxValue = newHealthComp.MaxHealth;
            }

            _target = value;
        }
    }

    [Export] private Vector2 _targetPosOffset = new Vector2(0, 20);
    private void OnMaxHealthChanged(double newvalue)
    {
        MaxValue = newvalue;
    }

    private void OnHealthChanged(double newvalue)
    {
        Value = newvalue;
    }


    public override void _Process(double delta)
    {
        base._Process(delta);

        if (_target == null) return;

        var screenPos = _target.GetScreenTransform().Origin;
        Position = screenPos + _targetPosOffset;
    }
}