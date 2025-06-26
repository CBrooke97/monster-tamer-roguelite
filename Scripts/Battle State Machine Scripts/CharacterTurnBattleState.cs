using Godot;
using System;
using Godot.Collections;
using MonsterTamerRoguelite.Scripts;

public partial class CharacterTurnBattleState : BattleState
{
    private TurnController? _turnController;
    private TurnCommand? _currentAction;

    private int turnQueueIndex = 0;
    private int _actionPoints = 1;
    
    public CharacterTurnBattleState(BattleStateMachine battleStateMachine) : base(battleStateMachine) { }

    public override void Enter()
    {
        _turnController = GetTurnController(BattleStateMachine.ActiveChar);

        Camera2D? camera2D = BattleStateMachine.ActiveChar.GetViewport().GetCamera2D();

        if (camera2D != null)
        {
            BattleStateMachine.ActiveChar.AddChild(camera2D);
        }

        turnQueueIndex = 0;
        _actionPoints = 1;

        GD.Print("CharTurnState Entered");
    }

    public override BattleState? Tick()
    {
        if (_turnController == null)
        {
            return BattleStateMachine.StartBattleState;   
        }

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
            TurnContext turnContext = new()
            {
                activeChar = BattleStateMachine.ActiveChar
            };

            _currentAction = _turnController.ExecuteTurn(turnContext);

            _currentAction?.Execute();
        }
        
        if (_actionPoints <= 0)
        {
            turnQueueIndex++;
            
            if (turnQueueIndex >= BattleStateMachine.turnQueue.Count)
            {
                return BattleStateMachine.StartRoundBattleState;
            }
            
            BattleStateMachine.ActiveChar = BattleStateMachine.turnQueue[turnQueueIndex];
            _turnController = GetTurnController(BattleStateMachine.ActiveChar);
            _actionPoints = 1;
        }
        
        return null;
    }

    public override void Exit()
    {
        _turnController = null;
        
        GD.Print("CharTurnState Exited");
    }
    
    private TurnController? GetTurnController(CharacterBody2D character)
    {
        Array<Node> activeCharChildNodes = character.GetChildren();

        foreach (Node child in activeCharChildNodes)
        {
            if (child is not TurnController turnController) continue;

            return turnController;
        }

        return null;
    }
}
