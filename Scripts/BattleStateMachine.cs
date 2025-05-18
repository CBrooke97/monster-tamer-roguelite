using Godot;

namespace MonsterTamerRoguelite.Scripts;

public partial class BattleStateMachine : Node
{
    public IState CurrentState;
    public override void _Ready()
    {
        base._Ready();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
    }
}