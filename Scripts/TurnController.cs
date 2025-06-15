using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;
using Godot.Collections;

namespace MonsterTamerRoguelite.Scripts;

[GlobalClass]
public partial class TurnController : Node
{
    [Export] private Array<Node> _actions;

    [Export] private int _actionPoints = 1;
    public bool ExecuteTurn()
    {
        foreach(ITurnAction act in _actions)
        {
            bool actionTaken = act.Execute();
            
            if (!actionTaken)
                continue;

            _actionPoints--;

            if (_actionPoints <= 0)
                break;
        }
        
        GD.Print(_actionPoints);
        
        return _actionPoints <= 0;
    }

    public void Reset()
    {
        _actionPoints = 1;
    }

    public override void _Ready()
    {
        base._Ready();

        foreach (Node node in _actions)
        {
            if (node is ITurnAction action)
            {
                continue;
            }

            _actions.Remove(node);
        }
    }
}