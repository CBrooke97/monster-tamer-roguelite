using Godot;
using Godot.Collections;
using System.Diagnostics;
using MonsterTamerRoguelite.Scenes.Levels;

namespace MonsterTamerRoguelite.Scripts;

[GlobalClass]
public partial class RTSController : Node2D
{
    [Export] private Texture2D _selectTexture;
    
    [Export] private Node2D _character;

    [Export] private PathFindingGridGenerator pathfinder;
    
    private Array<Vector2> path = new();

    private const string WeightScaleLayerName = "weight_scale";
    public override void _Ready()
    {
        base._Ready();
        
        Debug.Assert(_selectTexture != null, "Select Texture is null! Please set a Select Texture on the RTS Controller.");
        Debug.Assert(_character != null, "Character is null! Please set a Character on the RTS Controller.");
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        path = pathfinder.GetNavigablePath(_character.Position, GetGlobalMousePosition());
        
        if(path.Count > 0)
            QueueRedraw();
    }

    public override void _Draw()
    {
        base._Draw();

        foreach (var point in path)
        {
            DrawTextureRect(_selectTexture, new Rect2(point, new Vector2(16, 16)), false);
        }
    }
}