using System;
using System.Collections.Generic;

namespace MonsterTamerRoguelite.Scripts;

public class BasicAttributeModifier : AttributeModifier
{
    private Func<float, float> _operation;

    public BasicAttributeModifier(int duration, Func<float, float> operation) : base(duration)
    {
        _operation = operation;
    }

    public override float Handle(float value)
    {
        return _operation(value);
    }
}