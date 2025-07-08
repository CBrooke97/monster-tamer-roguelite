using System.Collections.Generic;
using System.Linq;
using Godot;
using Godot.Collections;

namespace MonsterTamerRoguelite.Scripts;

[GlobalClass]
public partial class BattleBootstrapper : Node
{
    [Export] private Array<Node> _characterStates;
    
    [Export] private PackedScene _monsterTemplateScene;
    private BattleStateMachine _battleStateMachine;

    public override void _Ready()
    {
        List<MTRCharacter> characterInstances = new List<MTRCharacter>();
        
        foreach (Node characterState in _characterStates)
        {
            MonsterTeamComponent? mtc = characterState.GetNodeOrNull<MonsterTeamComponent>("MonsterTeamComponent");

            if (mtc == null)
            {
                GD.PrintErr($"The character state '{characterState.Name}' does not have a monster team component!");
                continue;
            }
            
            mtc.OnEnterBattle();

            characterInstances.AddRange(mtc.CharacterInstances);
        }

        var context = new BattleContext(characterInstances);
        
        _battleStateMachine = new BattleStateMachine(context);
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        
        _battleStateMachine.Tick();
    }
}