namespace MonsterTamerRoguelite.Scripts;

public class StartBattleState : IState
{
    private BattleStateMachine _battleStateMachine;
    public StartBattleState(BattleStateMachine battleStateMachine)
    {
        _battleStateMachine = battleStateMachine;
    }
    
    public void Enter()
    {
        
    }

    public IState? Tick()
    {
        return null;
    }

    public void Exit()
    {
        
    }
}