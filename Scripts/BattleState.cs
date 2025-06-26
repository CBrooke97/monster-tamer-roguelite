namespace MonsterTamerRoguelite.Scripts;

public abstract class BattleState
{
    protected readonly BattleStateMachine BattleStateMachine;

    protected BattleState(BattleStateMachine battleStateMachine)
    {
        BattleStateMachine = battleStateMachine;
    }

    public abstract void Enter();
    public abstract BattleState? Tick();
    public abstract void Exit();
}