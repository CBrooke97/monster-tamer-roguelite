using Godot;

namespace MonsterTamerRoguelite.Scripts;

[GlobalClass]
public partial class PlayerTurnController : Node, ITurnController
{
    private CharacterBody2D? _owner;

    public override void _Ready()
    {
        base._Ready();

        _owner = GetParent<CharacterBody2D>();
    }

    public ITurnAction? ExecuteTurn()
    {
        if (_owner == null)
            return null;

        Vector2 moveDir = Input.GetVector("MoveWest", "MoveEast", "MoveNorth", "MoveSouth");
        
        if(moveDir.LengthSquared() > 0)
        {
            return new MoveTurnAction(_owner, moveDir);
        }
        
        // Return null if no valid actions taken.
        return null;
    }
}