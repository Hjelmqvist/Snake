using System.Collections.Generic;
using UnityEngine;
using Hjelmqvist.Pathfinding;

public class AIController : MonoBehaviour
{
    [SerializeField] GridManager _grid;
    [SerializeField] FruitManager _fruitManager;
    [SerializeField] SnakeManager _snake;

    [Space( 10 )]
    [SerializeField] float _timeBetweenMoves = 0.2f;
    float _timeSinceLastMove = 0;

    private void Update()
    {
        _timeSinceLastMove += Time.deltaTime;
        if (_timeSinceLastMove >= _timeBetweenMoves)
        {
            _timeSinceLastMove -= _timeBetweenMoves;
            ChangeDirection();
            _snake.Move();
        }
    }

    private void ChangeDirection()
    {
        Tile currentTile = _grid.GetTile( _snake.CurrentPosition );

        if (Pathfinding.AStar.TryGetPath( currentTile, _fruitManager.FruitTile, out List<Vector2Int> path ))
        {
            // If there's at least two positions then move towards the second position
            if (path.Count >= 2)
                _snake.ChangeDirection( path[1] - path[0] );
        }
        else
        {
            // Move to any connecting walkable tile
            foreach (IPathable pathable in currentTile.Connections)
            {
                if (pathable.IsWalkable())
                    _snake.ChangeDirection( pathable.Position - currentTile.Position );
            }
        }
    }
}