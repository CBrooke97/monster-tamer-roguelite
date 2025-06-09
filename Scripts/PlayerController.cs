using Godot;

namespace MonsterTamerRoguelite.Scripts;

[GlobalClass]
public partial class MovementComponent : Node
{
    private CharacterBody2D? _owner;
    private AnimationTree? _animTree;

    [Export] public float MoveSpeed { get; private set; } = 10.0f;

    public override void _Ready()
    {
        base._Ready();

        _owner = GetParent<CharacterBody2D>();

        _animTree = _owner.GetNode<AnimationTree>("AnimationTree");
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        
        
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 inputVector = Input.GetVector("MoveWest", "MoveEast", "MoveNorth", "MoveSouth");
        GD.Print(inputVector);
        bool idle = inputVector.LengthSquared() <= 0;

        _animTree.Set("parameters/conditions/Idle", idle);
        _animTree.Set("parameters/conditions/Run", !idle);

        if (!idle)
        {
            // When using MoveAndSlide, we do not need to apply delta.
            // Godot handles this internally.
            // If using MoveAndCollide, then you must apply delta manually:
            // player.MoveAndCollide(movementVector * (float)delta);
            Vector2 movementVector = inputVector * MoveSpeed;

            _owner.Velocity = movementVector; // Don't add to it — just set it
            _owner.MoveAndSlide();
        
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