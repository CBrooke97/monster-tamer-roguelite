using Godot;
using MonsterTamerRoguelite.Scripts;

namespace MonsterTamerRoguelite.Scripts;

public class MoveTurnCommand : TurnCommand
{
    private readonly MovementComponent? _movementComponent;
    private readonly Vector2 _dir;

    public MoveTurnCommand(MTRCharacter character, Vector2 dir)
    {
        _dir = dir;

        _movementComponent = character.GetNodeOrNull<MovementComponent>("MovementComponent");
    }

    public override bool Execute()
    {
        if (_movementComponent == null)
        {
            GD.Print($"No valid PlayerController Node attached when attempting to execute MoveTurnAction!");
            return false;
        }

        return _movementComponent.TryMove(_dir);
    }

    public override bool IsComplete()
    {
        return _movementComponent != null && !_movementComponent.IsMoving;
    }
}