using Hjelmqvist.Pathfinding;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "New AI Input", menuName = "Snake Input/AI Input" )]
public class AIInputSO : SnakeInputSO
{
    public override void UpdateValues()
    {
        // Update AI input when getting directions instead to minimalize use of pathfinding
    }

    public override Vector2Int GetDirection()
    {
        Tile currentTile = _grid.GetTile( _snake.CurrentPosition );

        if (Pathfinding.AStar.TryGetPath( currentTile, _fruitManager.FruitTile, out List<Vector2Int> path ))
        {
            // If there's at least two positions then move towards the second position
            if (path.Count >= 2)
                _currentDirection = path[1] - path[0];
        }
        else
        {
            // Move to any connecting walkable tile
            foreach (IPathable pathable in currentTile.Connections)
            {
                if (pathable.IsWalkable())
                    _currentDirection = pathable.Position - currentTile.Position;
            }
        }

        return _currentDirection;
    }
}