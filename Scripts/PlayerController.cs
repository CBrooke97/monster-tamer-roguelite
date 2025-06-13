using System.Diagnostics;
using Godot;

namespace MonsterTamerRoguelite.Scripts;

[GlobalClass]
public partial class PlayerController : Node
{
    [Export] public Pathfinder? _pathfinder;
    
    private CharacterBody2D? _owner;
    private AnimationTree? _animTree;

    private Vector2 _targetTilePos;

    [Export] public float MoveSpeed { get; private set; } = 10.0f;

    public override void _Ready()
    {
        base._Ready();
        
        Debug.Assert(_pathfinder != null, "Pathfinder is null! Please set a Pathfinder on the PlayerController.");

        _owner = GetParent<CharacterBody2D>();

        _animTree = _owner.GetNode<AnimationTree>("AnimationTree");

        _owner.Position = _pathfinder.GetClosestTileWorldPos(_owner.Position);
        _targetTilePos = _owner.Position;
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
        
        bool idle = _owner.Position == _targetTilePos;
        bool movementInput = inputVector.LengthSquared() > 0;

        _animTree.Set("parameters/conditions/Idle", idle);
        _animTree.Set("parameters/conditions/Run", !idle);

        if (idle && movementInput)
        {
            _targetTilePos = _owner.Position + inputVector * _pathfinder.GetTileSize();
            
            // When using MoveAndSlide, we do not need to apply delta.
            // Godot handles this internally.
            // If using MoveAndCollide, then you must apply delta manually:
            // player.MoveAndCollide(movementVector * (float)delta);
            if(_pathfinder.GetIsTileSolidForPos(_targetTilePos))
            {
                _targetTilePos = _owner.Position;
            }
        
            _animTree.Set("parameters/Run/blend_position", inputVector);
            _animTree.Set("parameters/Idle/blend_position", inputVector);
        }
        else
        {
            _animTree.Set("parameters/Run/blend_position", inputVector);
            _animTree.Set("parameters/Idle/blend_position", inputVector);
        }

        _owner.Position = _owner.Position.MoveToward(_targetTilePos, MoveSpeed * (float)delta); 
    }
}