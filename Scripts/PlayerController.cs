using System.Diagnostics;
using Godot;

namespace MonsterTamerRoguelite.Scripts;

[GlobalClass]
public partial class PlayerController : Node
{
    [Export] public Pathfinder? _pathfinder;
    
    private CharacterBody2D? _owner;
    private AnimationTree? _animTree;

    [Export] public float MoveSpeed { get; private set; } = 10.0f;

    public override void _Ready()
    {
        base._Ready();
        
        Debug.Assert(_pathfinder != null, "Pathfinder is null! Please set a Pathfinder on the PlayerController.");

        _owner = GetParent<CharacterBody2D>();

        _animTree = _owner.GetNode<AnimationTree>("AnimationTree");
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        
        
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 inputVector = Input.GetVector("MoveWest", "MoveEast", "MoveNorth", "MoveSouth").Round();
        bool idle = inputVector.LengthSquared() <= 0;
        float tileWeightScale =
          _pathfinder.GetTileWeightScaleForPos(_owner.Position + inputVector * _pathfinder.GetTileSize());

        _animTree.Set("parameters/conditions/Idle", idle);
        _animTree.Set("parameters/conditions/Run", !idle);

        if (!idle)
        {
            // When using MoveAndSlide, we do not need to apply delta.
            // Godot handles this internally.
            // If using MoveAndCollide, then you must apply delta manually:
            // player.MoveAndCollide(movementVector * (float)delta);
            if(!_pathfinder.GetIsTileSolidForPos(_owner.Position + inputVector * _pathfinder.GetTileSize()))
            {
                Vector2 movementVector = inputVector * MoveSpeed;

                _owner.Velocity = movementVector; // Don't add to it — just set it
                _owner.MoveAndSlide();
            }
        
            _animTree.Set("parameters/Run/blend_position", inputVector);
            _animTree.Set("parameters/Idle/blend_position", inputVector);
        }
        else
        {
            _owner.Velocity = Vector2.Zero;
            _owner.MoveAndSlide();
        }
    }
}