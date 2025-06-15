using Godot;

namespace MonsterTamerRoguelite.Scripts;

public class StartBattleState : IState
{
    private BattleStateMachine _battleStateMachine;

    private int WaitFrames = 60;
    public StartBattleState(BattleStateMachine battleStateMachine)
    {
        _battleStateMachine = battleStateMachine;
    }
    
    public void Enter()
    {
        WaitFrames = 60;
        GD.Print("StartBattleState Entered");
    }

    public IState? Tick()
    {
        IState? newState = null;

        WaitFrames--;

        if (WaitFrames <= 0)
        {
            newState = _battleStateMachine.CharTurnState;
        }
        
        return newState;
    }

    public void Exit()
    {
        GD.Print("StartBattleState Exited");
    }
}