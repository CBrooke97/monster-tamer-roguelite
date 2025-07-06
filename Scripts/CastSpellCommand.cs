namespace MonsterTamerRoguelite.Scripts;

public class CastSpellCommand : TurnCommand
{
    private readonly TargetData _data;
    private readonly IDeliveryStrategy _delivery;
    private bool _complete = false;
    
    public CastSpellCommand(TargetData data, IDeliveryStrategy delivery)
    {
        _data = data;
        _delivery = delivery;
    }

    public override bool Execute()
    {
        _delivery.Execute(_data,
            data => { _complete = true; });

        return false;
    }

    public override bool IsComplete()
    {
        return _complete;
    }
}