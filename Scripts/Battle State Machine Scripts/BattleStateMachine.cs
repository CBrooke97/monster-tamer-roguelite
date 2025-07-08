using System.Linq;
using Godot;
using Godot.Collections;

namespace MonsterTamerRoguelite.Scripts;

public partial class BattleStateMachine
{
    // States
    #region States
    private BattleState? _currentState;
    
    public readonly StartBattleState StartBattleState;
    public readonly StartRoundBattleState StartRoundBattleState;
    public readonly CharacterTurnBattleState CharTurnBattleState;
    public readonly EndRoundBattleState EndRoundBattleState;

    public readonly BattleContext BattleContext;
#endregion

    public BattleStateMachine(BattleContext battleContext)
    {
        BattleContext = battleContext;
        
        StartBattleState = new StartBattleState(this);
        StartRoundBattleState = new StartRoundBattleState(this);
        CharTurnBattleState = new CharacterTurnBattleState(this);
        EndRoundBattleState = new EndRoundBattleState(this);
        
        _currentState = StartBattleState;
        _currentState.Enter();
    }

    public void Tick()
    {
        BattleState? newState = _currentState?.Tick();

        if (newState != null)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState?.Enter();
        }
    }
}