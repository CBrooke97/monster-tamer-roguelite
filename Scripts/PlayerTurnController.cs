using Godot;

namespace MonsterTamerRoguelite.Scripts;

[GlobalClass]
public partial class PlayerTurnController : TurnController
{
    private CharacterBody2D? _owner;

    public override void _Ready()
    {
        base._Ready();

        _owner = GetParent<CharacterBody2D>();
    }

    public override TurnCommand? ExecuteTurn(TurnContext turnContext)
    {
        if (_owner == null)
            return null;

        Vector2 moveDir = Input.GetVector("MoveWest", "MoveEast", "MoveNorth", "MoveSouth");
        
        if(moveDir.LengthSquared() > 0)
        {
            return new MoveTurnCommand(turnContext.activeChar, moveDir);
        }
        
        // Return null if no valid actions taken.
        return null;
    }     
}