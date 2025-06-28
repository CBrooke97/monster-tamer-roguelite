using Godot;

namespace MonsterTamerRoguelite.Scripts;

[GlobalClass]
public partial class PlayerTurnController : TurnController
{
    private Node? _owner;

    public override void _Ready()
    {
        base._Ready();

        _owner = GetParent<Node>();
    }

    public override TurnCommand? ExecuteTurn(TurnContext turnContext)
    {
        if (_owner == null)
            return null;

        Vector2 moveDir = Input.GetVector("MoveWest", "MoveEast", "MoveNorth", "MoveSouth");
        
        if(moveDir.LengthSquared() > 0)
        {
            return new MoveTurnCommand(turnContext.ActiveChar, moveDir);
        }
        
        // Return null if no valid actions taken.
        return null;
    }     
}