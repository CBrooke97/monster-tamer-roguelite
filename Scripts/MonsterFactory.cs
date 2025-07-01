using Godot;

namespace MonsterTamerRoguelite.Scripts;

public static class MTRCharacterFactory
{
    private static PackedScene _mtrCharacterScene = ResourceLoader.Load<PackedScene>("res://Scenes/EnemyChar.tscn");

    public static MTRCharacter Create(MonsterDefinitionResource definition, TurnController turnController)
    {
        MTRCharacter mtrCharacter = _mtrCharacterScene.Instantiate<MTRCharacter>();
        
        mtrCharacter.Init(definition, turnController);

        Sprite2D? sprite2D = mtrCharacter.GetNodeOrNull<Sprite2D>("Sprite2D");
        if (sprite2D != null)
        {
            sprite2D.SetTexture(definition.Texture2D);
            sprite2D.SetHframes(definition.HFrames);
            sprite2D.SetVframes(definition.VFrames);

            Vector2 spriteSize = definition.Texture2D.GetSize();
            Vector2 frameSize = new Vector2(spriteSize.X / definition.HFrames, spriteSize.Y / definition.VFrames);
            Vector2 spriteScale = new Vector2(16, 16) / frameSize;
            
            sprite2D.SetScale(spriteScale);
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

        return mtrCharacter;
    }
}