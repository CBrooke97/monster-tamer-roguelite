using System;
using Godot;
using Godot.Collections;

namespace MonsterTamerRoguelite.Scenes.Levels;

public partial class PathFindingGridGenerator : Node
{
    private Array<TileMapLayer> _tileMapLayers;

    private AStarGrid2D _aStarGrid2D;

    private Array<Vector2I> path = new();

    private const string WeightScaleLayerName = "weight_scale";
    
    public override void _Ready()
    {
        base._Ready();

        _tileMapLayers = new();
        
        Array<Node> children = GetChildren();

        foreach (Node child in children)
        {
            if (child is TileMapLayer layer)
            {
                _tileMapLayers.Add(layer);
            }
        }

        Rect2I usedRect = new Rect2I();
        foreach (var tilemap in _tileMapLayers)
        {
            Rect2I tileRect = tilemap.GetUsedRect();
            usedRect = tileRect.Area > usedRect.Area ? tileRect : usedRect;
        }

        _aStarGrid2D = new AStarGrid2D();
        
        _aStarGrid2D.Region = usedRect;
        _aStarGrid2D.CellSize = new Vector2I(16, 16);
        _aStarGrid2D.DiagonalMode = AStarGrid2D.DiagonalModeEnum.Never;
        _aStarGrid2D.DefaultComputeHeuristic = AStarGrid2D.Heuristic.Manhattan;
        _aStarGrid2D.DefaultEstimateHeuristic = AStarGrid2D.Heuristic.Manhattan;
        _aStarGrid2D.Update();
        
        foreach (var tilemap in _tileMapLayers)
        {
            foreach(var tileCoords in tilemap.GetUsedCells())
            {
                var tile = tilemap.GetCellTileData(tileCoords);

                if (!tile.HasCustomData(WeightScaleLayerName))
                    continue;

                float weightscale = (float)tile.GetCustomData(WeightScaleLayerName);

                if (weightscale < 0.0f)
                {
                    _aStarGrid2D.SetPointSolid(tileCoords, true);
                }
                else
                {
                    _aStarGrid2D.SetPointWeightScale(tileCoords, weightscale);
                }
            }
        }
    }
    
    public Array<Vector2> GetNavigablePath(Vector2 startWorld, Vector2 endWorld)
    {
        Array<Vector2> navPath = new();

        Vector2I startLocal = (Vector2I)(startWorld / _aStarGrid2D.CellSize).Floor();
        Vector2I endLocal = (Vector2I)(endWorld / _aStarGrid2D.CellSize).Floor();
        
        if (!_aStarGrid2D.IsInBoundsv(startLocal) || !_aStarGrid2D.IsInBoundsv(endLocal))
        {
            return navPath;
        }
        
        Array<Vector2I> localPath = _aStarGrid2D.GetIdPath(startLocal, endLocal);

        foreach (Vector2I localPoint in localPath)
        {
            Vector2 worldPoint = _aStarGrid2D.CellSize * localPoint;
            navPath.Add(worldPoint);
        }

        return navPath;
    }
}