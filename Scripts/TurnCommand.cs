namespace MonsterTamerRoguelite.Scripts;

public abstract class TurnCommand
{
    public abstract bool Execute();

    public abstract bool IsComplete();
}