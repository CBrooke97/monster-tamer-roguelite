using Godot;
using Godot.Collections;

namespace MonsterTamerRoguelite.Scripts;

[GlobalClass]
public partial class MonsterTeamComponent : Node
{
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
}