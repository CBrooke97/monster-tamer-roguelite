using Godot;

namespace MonsterTamerRoguelite.Scripts;

[GlobalClass]
public partial class AITurnController : TurnController
{
    public override TurnCommand? ExecuteTurn(TurnContext turnContext)
    {
        float rand = GD.Randf();
        Vector2 randDir = new Vector2(rand, 1.0f - rand);
        randDir.Round();

        return new MoveTurnCommand(turnContext.activeChar, randDir);
    }
}