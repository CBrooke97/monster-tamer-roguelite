using System.Diagnostics;
using System.Reflection.PortableExecutable;
using Godot;

namespace MonsterTamerRoguelite.Scripts;

[GlobalClass]
public partial class PlayerController : Node
{
    [Export] public Pathfinder? _pathfinder;
    
    private CharacterBody2D? _owner;
    private AnimationTree? _animTree;

    private Vector2 _targetTilePos;

    [Export] private bool _inputEnabled = true;

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
        if(_inputEnabled)
        {
            Vector2 inputVector = Input.GetVector("MoveWest", "MoveEast", "MoveNorth", "MoveSouth");

            TryMove(inputVector);
        }
        
        _owner.Position = _owner.Position.MoveToward(_targetTilePos, MoveSpeed * (float)delta); 
    }

    public bool TryMove(Vector2 dir)
    {
        Vector2 roundedDir = dir.Round(); 
        
        bool hasMovementInput = roundedDir.LengthSquared() > 0;

        if (!hasMovementInput)
        {
            return false;
        }
        
        _animTree.Set("parameters/conditions/Idle", !hasMovementInput);
        _animTree.Set("parameters/conditions/Run", hasMovementInput);
        
        bool idle = _owner.Position == _targetTilePos;

        if (idle && hasMovementInput)
        {
            _targetTilePos = _owner.Position + roundedDir * _pathfinder.GetTileSize();
            
            _animTree.Set("parameters/Run/blend_position", roundedDir);
            _animTree.Set("parameters/Idle/blend_position", roundedDir);
            
            if(_pathfinder.GetIsTileSolidForPos(_targetTilePos))
            {
                _targetTilePos = _owner.Position;
                return false;
            }
        }

        return true;
    }
}