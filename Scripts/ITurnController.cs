namespace MonsterTamerRoguelite.Scripts;

public interface ITurnController
{
    public TurnCommand? ExecuteTurn(TurnContext turnContext);
}