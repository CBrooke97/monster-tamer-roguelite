namespace MonsterTamerRoguelite.Scripts;

public interface IState
{
    public void Enter();
    public IState Tick();
    public void Exit();
}