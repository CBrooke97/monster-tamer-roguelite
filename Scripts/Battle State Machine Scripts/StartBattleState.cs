using Godot;
using Godot.Collections;

namespace MonsterTamerRoguelite.Scripts;

public class StartBattleState : BattleState
{
    private int WaitFrames = 60;
    
    public StartBattleState(BattleStateMachine battleStateMachine) : base(battleStateMachine)
    {
        
    }

    public override void Enter()
    {
        BattleStateMachine.TurnQueue = new Array<MTRCharacter>();

        foreach (Node characterState in BattleStateMachine.CharacterStates)
        {
            MonsterTeamComponent? mtc = characterState.GetNodeOrNull<MonsterTeamComponent>("MonsterTeamComponent");

            if (mtc == null)
            {
                GD.PrintErr($"The character state '{characterState.Name}' does not have a monster team component!");
                continue;
            }
            
            mtc.OnEnterBattle();

            BattleStateMachine.TurnQueue += mtc.CharacterInstances;
        }
        
        BattleStateMachine.TurnQueue.Shuffle();
        
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