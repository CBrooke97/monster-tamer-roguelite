using Godot;

namespace MonsterTamerRoguelite.Scripts;

public partial class Spell : Resource
{
    [Export]
    private Godot.Collections.Dictionary<EAttributeType, float> AttributeModifiers;
}