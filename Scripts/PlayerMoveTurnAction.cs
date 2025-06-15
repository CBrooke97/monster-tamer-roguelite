using Godot;
using System;
using MonsterTamerRoguelite.Scripts;

[GlobalClass]
public partial class PlayerMoveTurnAction : Node, ITurnAction
{
    [Export] private PlayerController? _playerController;
    public bool Execute()
    {
        Vector2 inputVector = Input.GetVector("MoveWest", "MoveEast", "MoveNorth", "MoveSouth");

        if (_playerController == null)
            return false;

        return _playerController.TryMove(inputVector);
    }
}
