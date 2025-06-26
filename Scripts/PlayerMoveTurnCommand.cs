using System;
using Godot;

namespace MonsterTamerRoguelite.Scripts;

public partial class PlayerMoveTurnCommand : TurnCommand
{
    [Export] private PlayerController? _playerController;

    public override bool Execute()
    {
        throw new NotImplementedException();
    }

    public override bool IsComplete()
    {
        throw new NotImplementedException();
    }
}