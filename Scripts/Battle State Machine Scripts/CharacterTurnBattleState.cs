using Godot;
using System;
using Godot.Collections;
using MonsterTamerRoguelite.Scripts;

public partial class CharacterTurnBattleState : BattleState
{
    private TurnController? _turnController;
    private TurnCommand? _currentAction;
    
    private int _actionPoints = 1;
    
    public CharacterTurnBattleState(BattleStateMachine battleStateMachine) : base(battleStateMachine) { }

    public override void Enter()
    {
        var context = BattleStateMachine.BattleContext;
        
        _turnController = context.ActiveCharacter.TurnController;

        Camera2D? camera2D = context.ActiveCharacter.GetViewport().GetCamera2D();

        if (camera2D != null)
        {
            camera2D.GetParent().RemoveChild(camera2D);
            context.ActiveCharacter.AddChild(camera2D);
        }
        
        _actionPoints = 1;

        GD.Print("CharTurnState Entered");
    }

    public override BattleState? Tick()
    {
        if (_turnController == null)
        {
            return BattleStateMachine.StartBattleState;   
        }

        var context = BattleStateMachine.BattleContext;

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
                ActiveChar = context.ActiveCharacter
            };

            _currentAction = _turnController.ExecuteTurn(turnContext);

            _currentAction?.Execute();
        }
        
        if (_actionPoints <= 0)
        {
            bool newRoundNeeded = context.AdvanceTurn();
            
            if (newRoundNeeded)
            {
                return BattleStateMachine.EndRoundBattleState;
            }
            
            // Returning the same state as current to restart with new char, saves logic duplication.
            return BattleStateMachine.CharTurnBattleState;
        }
        
        return null;
    }

    public override void Exit()
    {
        _turnController = null;
        
        GD.Print("CharTurnState Exited");
    }
}
