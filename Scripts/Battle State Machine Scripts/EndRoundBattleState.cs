using Godot;

namespace MonsterTamerRoguelite.Scripts;

public class EndRoundBattleState : BattleState
{
    public EndRoundBattleState(BattleStateMachine battleStateMachine) : base(battleStateMachine)
    {
    }

    public override void Enter()
    {
        GD.Print("Entered End Round Battle State.");
    }

    public override BattleState? Tick()
    {
        return BattleStateMachine.StartRoundBattleState;
    }

    public override void Exit()
    {
        GD.Print("Exited End Round Battle State.");
    }
}