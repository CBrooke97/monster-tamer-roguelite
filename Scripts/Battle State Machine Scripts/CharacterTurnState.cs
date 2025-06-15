using Godot;
using System;
using MonsterTamerRoguelite.Scripts;

public partial class CharacterTurnState : IState
{
    private BattleStateMachine _battleStateMachine;

    private TurnController _turnController;
    public CharacterTurnState(BattleStateMachine battleStateMachine)
    {
        _battleStateMachine = battleStateMachine;
    }
    
    public void Enter()
    {
        _turnController = _battleStateMachine.ActiveChar.GetNode<TurnController>("TurnController");
        _turnController.Reset();
        GD.Print("CharTurnState Entered");
    }

    public IState? Tick()
    {
        IState? newState = null;
        
        bool turnFinished = _turnController.ExecuteTurn();
        
        if (turnFinished)
        {
            newState = _battleStateMachine.StartBattleState;
        }

        return newState;
    }

    public void Exit()
    {
        GD.Print("CharTurnState Exited");
    }
}
