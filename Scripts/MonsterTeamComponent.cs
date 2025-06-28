using Godot;
using Godot.Collections;

namespace MonsterTamerRoguelite.Scripts;

[GlobalClass]
public partial class MonsterTeamComponent : Node
{
    private Node? _owner;
    public Array<MTRCharacter> CharacterInstances { get; private set; } = new();

    [Export] private PackedScene _monsterObjTemplate = ResourceLoader.Load<PackedScene>("res://Scenes/EnemyChar.tscn");
    
    [Export]
    public Array<MonsterDefinitionResource?> MonsterTeam { get; private set; } = new Array<MonsterDefinitionResource?>()
    {
        null,
        null,
        null,
        null,
        null,
        null
    };

    public void OnEnterBattle(Pathfinder pathfinder)
    {
        var owner = GetParent();

        if (owner == null)
        {
            GD.PrintErr($"{Name} does not have a valid owner!");
            return;
        }
        
        TurnController? turnController = owner.GetNodeOrNull<TurnController>("TurnController");

        if (turnController == null)
        {
            GD.PrintErr($"{owner.Name} does not have a valid turn controller!");
            return;
        }
        
        foreach(var definition in MonsterTeam)
        {
            if (definition == null)
                continue;
            
            MTRCharacter character = MTRCharacterFactory.Create(definition, turnController, pathfinder);
            CharacterInstances.Add(character);
            owner.AddChild(character);
        }
    }

    public override void _Ready()
    {
        base._Ready();
        
    }
}