using Godot;

namespace MonsterTamerRoguelite.Scripts;

public static class MTRCharacterFactory
{
    private static PackedScene _mtrCharacterScene = ResourceLoader.Load<PackedScene>("res://Scenes/EnemyChar.tscn");

    public static MTRCharacter Create(MonsterDefinitionResource definition, TurnController turnController, Pathfinder pathfinder)
    {
        MTRCharacter mtrCharacter = _mtrCharacterScene.Instantiate<MTRCharacter>();
        
        mtrCharacter.Init(definition, turnController);

        Sprite2D? sprite2D = mtrCharacter.GetNodeOrNull<Sprite2D>("Sprite2D");
        if (sprite2D != null)
        {
            sprite2D.SetTexture(definition.Texture2D);
        }
        
        AttributeComponent? attributeComp = mtrCharacter.GetNodeOrNull<AttributeComponent>("AttributeComponent");
        if (attributeComp != null)
        {
            attributeComp.SetBaseAttributes(definition.AttributeMap);
        }

        HealthComponent? healthComp = mtrCharacter.GetNodeOrNull<HealthComponent>("HealthComponent");
        if (healthComp != null)
        {
            healthComp.Init(definition.AttributeMap[EAttributeType.Health]);
        }

        MovementComponent? moveComp = mtrCharacter.GetNodeOrNull<MovementComponent>("MovementComponent");
        if (moveComp != null)
        {
            moveComp.Pathfinder = pathfinder;
        }

        return mtrCharacter;
    }
}