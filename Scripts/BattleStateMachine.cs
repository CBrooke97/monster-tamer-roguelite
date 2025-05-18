using Godot;

namespace MonsterTamerRoguelite.Scripts;

public partial class BattleStateMachine : Node
{
    public readonly StartBattleState StartBattleState;
    
    private IState _currentState;

    public BattleStateMachine()
    {
        StartBattleState = new StartBattleState(this);
        
        _currentState = StartBattleState;
        _currentState.Enter();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        IState? newState = _currentState.Tick();

        if (newState != null)
        {
            _currentState.Exit();
            _currentState = newState;
            _currentState.Enter();
        }
    }
}