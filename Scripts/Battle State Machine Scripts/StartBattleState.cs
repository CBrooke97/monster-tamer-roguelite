using Godot;

namespace MonsterTamerRoguelite.Scripts;

public class StartBattleState : BattleState
{
    private int WaitFrames = 60;
    
    public StartBattleState(BattleStateMachine battleStateMachine) : base(battleStateMachine)
    {
        
    }

    public override void Enter()
    {
        WaitFrames = 60;
        GD.Print("StartBattleState Entered");
    }

    public override BattleState? Tick()
    {
        BattleState? newState = null;

        WaitFrames--;

        if (WaitFrames <= 0)
        {
            newState = BattleStateMachine.StartRoundBattleState;
        }
        
        return newState;
    }

    public override void Exit()
    {
        GD.Print("StartBattleState Exited");
    }
}