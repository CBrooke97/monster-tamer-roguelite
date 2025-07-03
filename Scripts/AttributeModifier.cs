namespace MonsterTamerRoguelite.Scripts;

public abstract class AttributeModifier
{
    private int _duration = 0;
    public bool MarkForRemoval { get; private set; }
    
    public AttributeModifier(int turnDuration)
    {
        _duration = turnDuration;
    }

    public virtual void Tick()
    {
        _duration--;

        MarkForRemoval = _duration <= 0;
    }
    public abstract float Handle(float value);
}