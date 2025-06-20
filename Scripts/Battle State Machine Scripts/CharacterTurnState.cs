using Godot;
using System;
using Godot.Collections;
using MonsterTamerRoguelite.Scripts;

public partial class CharacterTurnState : IState
{
    private BattleStateMachine _battleStateMachine;

    private ITurnController? _turnController;
    private ITurnAction? _currentAction;

    private int _actionPoints = 1;
    public CharacterTurnState(BattleStateMachine battleStateMachine)
    {
        _battleStateMachine = battleStateMachine;
    }
    
    public void Enter()
    {
        Array<Node> activeCharChildNodes = _battleStateMachine.ActiveChar.GetChildren();

        foreach (Node child in activeCharChildNodes)
        {
            if (child is not ITurnController turnController) continue;
            
            _turnController = turnController;
            break;
        }

        _actionPoints = 1;
        
        GD.Print("CharTurnState Entered");
    }

    public IState? Tick()
    {
        if (_turnController == null)
        {
            return _battleStateMachine.StartBattleState;   
        }
        
        IState? newState = null;

        if (_currentAction != null)
        {
            if (_currentAction.IsComplete())
            {
                _currentAction = null;
                _actionPoints--;
            }
        }
        else
        {
            _currentAction = _turnController.ExecuteTurn();

            _currentAction?.Execute();
        }
        
        if (_actionPoints <= 0)
        {
            newState = _battleStateMachine.StartBattleState;
        }
        
        return newState;
    }

    public void Exit()
    {
        _turnController = null;
        
        GD.Print("CharTurnState Exited");
    }
}
