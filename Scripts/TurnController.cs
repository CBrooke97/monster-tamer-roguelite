using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;
using Godot.Collections;

namespace MonsterTamerRoguelite.Scripts;

[GlobalClass]
public abstract partial class TurnController : Node
{
    [Export] public CharacterBody2D? ControlledCharacter { get; private set; }

    public virtual void PossessCharacter(CharacterBody2D character)
    {
        ControlledCharacter = character;
    }
    
    /// Executes the turn logic for this controller.
    /// Returns a TurnCommand representing the chosen action, or null if no action is taken.
    /// <param name="turnContext">The current turn context containing battle state.</param>
    /// <returns>The chosen TurnCommand, or null.</returns>
    public abstract TurnCommand? ExecuteTurn(TurnContext turnContext);
}