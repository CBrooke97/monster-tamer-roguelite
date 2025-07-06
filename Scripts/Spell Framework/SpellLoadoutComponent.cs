using Godot;
using Godot.Collections;

namespace MonsterTamerRoguelite.Scripts.Spell_Framework;

[GlobalClass]
public partial class SpellLoadoutComponent : Node
{
    [Export] private Array<SpellDefinition> _activeSpells;

    public Array<SpellDefinition> ActiveSpells => _activeSpells;
    
    
}