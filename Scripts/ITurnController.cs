namespace MonsterTamerRoguelite.Scripts;

public interface ITurnController
{
    public ITurnAction? ExecuteTurn();
}