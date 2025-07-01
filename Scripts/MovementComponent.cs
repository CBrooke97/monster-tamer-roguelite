using System.Diagnostics;
using System.Reflection.PortableExecutable;
using Godot;

namespace MonsterTamerRoguelite.Scripts;

[GlobalClass]
public partial class MovementComponent : Node
{
    private CharacterBody2D? _owner;
    private AnimationTree? _animTree;

    private Vector2 _targetTilePos;

    public bool IsMoving => _owner?.Position != _targetTilePos;

    [Export] private bool _inputEnabled = true;

    [Export] public float MoveSpeed { get; private set; } = 30.0f;

    private bool _isMoving = false;

    public override void _Ready()
    {
        base._Ready();
        
        _owner = GetParent<CharacterBody2D>();

        _animTree = _owner.GetNode<AnimationTree>("AnimationTree");

        _owner.Position = Pathfinder.Instance.GetClosestTileWorldPos(_owner.Position);
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
        if (_owner == null)
            return;
        
        if(_inputEnabled)
        {
            Vector2 inputVector = Input.GetVector("MoveWest", "MoveEast", "MoveNorth", "MoveSouth");

            TryMove(inputVector);
        }

        HandleMovement(delta);
    }

    private void HandleMovement(double delta)
    {
        _isMoving = _targetTilePos != _owner.Position;

        _animTree.Set("parameters/conditions/Idle", !_isMoving);
        _animTree.Set("parameters/conditions/Run", _isMoving);

        if (_isMoving)
        {
            Vector2 dir = _targetTilePos - _owner.Position;

            _animTree.Set("parameters/Run/blend_position", dir);
            _animTree.Set("parameters/Idle/blend_position", dir);
            _owner.Position = _owner.Position.MoveToward(_targetTilePos, MoveSpeed * (float)delta);
        }
    }

    public bool TryMove(Vector2 dir)
    {
        Vector2 roundedDir = dir.Round(); 
        
        bool hasMovementInput = roundedDir.LengthSquared() > 0;

        if (!hasMovementInput)
        {
            return false;
        }

        if (!_isMoving && hasMovementInput)
        {
            _targetTilePos = _owner.Position + roundedDir * Pathfinder.Instance.GetTileSize();

            if(Pathfinder.Instance.GetIsTileSolidForPos(_targetTilePos))
            {
                _targetTilePos = _owner.Position;
                return false;
            }
        }

        return true;
    }
}