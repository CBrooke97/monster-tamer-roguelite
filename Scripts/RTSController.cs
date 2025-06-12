using System.Collections.Generic;
using Godot;
using Godot.Collections;
using System.Diagnostics;
using System.Linq;

namespace MonsterTamerRoguelite.Scripts;

[GlobalClass]
public partial class RTSController : Node2D
{
    [Signal] public delegate void OnCharacterPossessedEventHandler(); 
    
    [Export] private float movespeed = 100.0f;
    [Export] private Texture2D _selectTexture;
    
    [Export] private CharacterBody2D _character;

    [Export] private Pathfinder pathfinder;
    
    private Array<Vector2> path = new();

    private Queue<Vector2> targetPath = new();

    private Vector2 _tileSize;

    private const string WeightScaleLayerName = "weight_scale";
    public override void _Ready()
    {
        base._Ready();
        
        Debug.Assert(_selectTexture != null, "Select Texture is null! Please set a Select Texture on the RTS Controller.");
        Debug.Assert(_character != null, "Character is null! Please set a Character on the RTS Controller.");

        _tileSize = pathfinder.GetTileSize();
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        path = pathfinder.GetNavigablePath(_character.Position, GetGlobalMousePosition());

        if(path.Count > 0)
            QueueRedraw();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        
        if (targetPath.Count > 0)
        {
            _character.Position = _character.Position.MoveToward(targetPath.First(), (float)delta * movespeed);

            if (_character.Position.IsEqualApprox(targetPath.First()))
            {
                targetPath.Dequeue();
            }
        }
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);

        if (@event.IsActionReleased("Select"))
        {
            foreach(Vector2 point in path)
            {
                targetPath.Enqueue(point);
            }
        }
    }

    public override void _Draw()
    {
        base._Draw();

        foreach (var point in path)
        {
            DrawTextureRect(_selectTexture, new Rect2(point - (_tileSize / 2.0f), _tileSize), false);
        }
    }

    public void PossessCharacter(CharacterBody2D character)
    {
        _character = character;
        EmitSignal(SignalName.OnCharacterPossessed);
    }
}