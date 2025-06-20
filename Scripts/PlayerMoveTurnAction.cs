using Godot;
using System;
using MonsterTamerRoguelite.Scripts;

[GlobalClass]
public partial class PlayerMoveTurnAction : Node, ITurnAction
{
    [Export] private PlayerController? _playerController;

    public bool Execute()
    {
        throw new NotImplementedException();
    }

    public bool IsComplete()
    {
        throw new NotImplementedException();
    }
}
