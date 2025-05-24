using System.Collections.Generic;
using Godot;

namespace MonsterTamerRoguelite.Scripts;

public partial class Spell : Resource
{
    [Export]
    private Godot.Collections.Dictionary<EAttributeType, float>? _attributeModifiers;

    void ApplySpell(AttributeComponent target, AttributeComponent source)
    {
        if (_attributeModifiers != null)
        {
            foreach (KeyValuePair<EAttributeType, float> modifier in _attributeModifiers)
            {
                target.ApplyAttributeModifer(modifier.Key, modifier.Value);
            }
        }
    }
}