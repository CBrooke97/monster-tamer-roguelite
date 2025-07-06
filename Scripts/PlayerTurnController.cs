using Godot;
using MonsterTamerRoguelite.Scripts.Spell_Framework;

namespace MonsterTamerRoguelite.Scripts;

[GlobalClass]
public partial class PlayerTurnController : TurnController
{
    private Node? _owner;
    private SpellLoadoutComponent _spellLoadoutComponent;
    private ITargetingStrategy? _spellTargeting;
    private CastSpellCommand? _spellCommand;

    public override void _Ready()
    {
        base._Ready();

        _owner = GetParent<Node>();
    }

    public override TurnCommand? ExecuteTurn(TurnContext turnContext)
    {
        if (_owner == null)
            return null;
        
        HandleSpellInput(turnContext);

        if (_spellCommand != null)
        {
            _spellTargeting = null;
            var cmd = _spellCommand;
            _spellCommand = null;
            return cmd;
        }

        Vector2 moveDir = Input.GetVector("MoveWest", "MoveEast", "MoveNorth", "MoveSouth");
        
        if(moveDir.LengthSquared() > 0)
        {
            return new MoveTurnCommand(turnContext.ActiveChar, moveDir);
        }

        // Return null if no valid actions taken.
        return null;
    }

    private void HandleSpellInput(TurnContext turnContext)
    {
        if (_spellTargeting != null)
        {
            _spellTargeting.Tick();
            return;
        }
        
        if (Input.IsActionJustReleased("SpellOneKey"))
        {
            SpellDefinition activeSpell = turnContext.ActiveChar.GetNode<SpellLoadoutComponent>("SpellLoadoutComponent").ActiveSpells[0];
            _spellTargeting = activeSpell.TargetingStrategy;

            TargetData data = new();
            data.SourceUnit = turnContext.ActiveChar;
            
            _spellTargeting.BeginSelection(data,
                targetData =>
                {
                    _spellCommand = new CastSpellCommand(data, activeSpell.DeliveryStrategy);
                },
                () =>
                {
                    _spellTargeting = null;
                });
        }
    }
}