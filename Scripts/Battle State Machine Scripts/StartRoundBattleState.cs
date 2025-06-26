using Godot;

namespace MonsterTamerRoguelite.Scripts;

public class StartRoundBattleState : BattleState
{
    public StartRoundBattleState(BattleStateMachine battleStateMachine) : base(battleStateMachine)
    {
        
    }
    public override void Enter()
    {
        GD.Print("Entering Start Round State.");
        BattleStateMachine.ActiveChar = BattleStateMachine.turnQueue[0];
    }

    public override BattleState? Tick()
    {
        return BattleStateMachine.CharTurnBattleState;
    }

    public override void Exit()
    {
        GD.Print("Exiting Start Round State.");
    }


}