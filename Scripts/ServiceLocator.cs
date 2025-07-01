using Godot;

namespace MonsterTamerRoguelite.Scripts;

public partial class ServiceLocator : Node
{
    public static ServiceLocator Instance { get; private set; }

    public override void _EnterTree()
    {
        base._EnterTree();

        Instance = this;
    }
}