using System;
using Godot;

namespace MonsterTamerRoguelite.Scripts.Spell_Framework;

[GlobalClass]
public partial class ProjectileTargetingStrategy : Resource, ITargetingStrategy
{
    private TargetData _data;
    private Action<TargetData> _onConfirmCallback;
    private Action _onCancelCallback;

    private Vector2 _mouseDir = Vector2.Zero;
    public void BeginSelection(TargetData data, Action<TargetData> onConfirmCallback, Action onCancelCallback)
    {
        _data = data;
        _onConfirmCallback = onConfirmCallback;
        _onCancelCallback = onCancelCallback;
    }

    public void Tick()
    {
        if (_data.SourceUnit != null)
        {
            _mouseDir = _data.SourceUnit.GetViewport().GetMousePosition() - _data.SourceUnit.Position;
            _mouseDir = _mouseDir.Normalized();
        }

        if (Input.IsMouseButtonPressed(MouseButton.Left))
        {
            _data.Direction = (Vector2I)_mouseDir.Round();
            _onConfirmCallback(_data);
        }

        if (Input.IsKeyPressed(Key.Escape))
        {
            _onCancelCallback();
        }
    }
}