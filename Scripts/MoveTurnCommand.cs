using Godot;
using MonsterTamerRoguelite.Scripts;

namespace MonsterTamerRoguelite.Scripts;

public class MoveTurnCommand : TurnCommand
{
    private readonly PlayerController? _playerController;
    private readonly Vector2 _dir;

    public MoveTurnCommand(CharacterBody2D character, Vector2 dir)
    {
        _dir = dir;
        _playerController = character.GetNode<PlayerController>("PlayerController");
    }

    public override bool Execute()
    {
        if (_playerController == null)
        {
            GD.Print($"No valid PlayerController Node attached when attempting to execute MoveTurnAction!");
            return false;
        }

        return _playerController.TryMove(_dir);
    }

    public override bool IsComplete()
    {
        return _playerController != null && !_playerController.IsMoving;
    }
}