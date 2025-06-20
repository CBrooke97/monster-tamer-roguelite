namespace MonsterTamerRoguelite.Scripts;

public interface ITurnAction
{
    bool Execute();

    bool IsComplete();
}