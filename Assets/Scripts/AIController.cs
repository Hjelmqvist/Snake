using System.Collections.Generic;
using UnityEngine;
using Hjelmqvist.Pathfinding;
using UnityEngine.Profiling;

public class AIController : MonoBehaviour
{
    [SerializeField] GridManager _grid;
    [SerializeField] FruitManager _fruitManager;
    [SerializeField] SnakeController _snake;

    [Space( 10 )]
    [SerializeField] float _timeBetweenMoves = 0.2f;
    float _timeSinceLastMove = 0;

    private void Update()
    {
        _timeSinceLastMove += Time.deltaTime;
        if (_timeSinceLastMove >= _timeBetweenMoves)
        {
            _timeSinceLastMove -= _timeBetweenMoves;

            Tile currentTile = _grid.GetTile( _snake.CurrentPosition );

            Profiler.BeginSample( "AStar pathfinding" );
            if (Pathfinding.AStar.TryGetPath( currentTile, _fruitManager.FruitTile, out List<Vector2Int> path ))
            {
                Profiler.EndSample();
                if (path.Count > 1)
                    _snake.ChangeDirection( path[1] - path[0] );
            }
            else
            {
                Profiler.EndSample();
                // Move to any connecting walkable tile
                foreach (IPathable pathable in currentTile.Connections)
                {
                    if (pathable.IsWalkable())
                        _snake.ChangeDirection( pathable.Position - currentTile.Position );
                }
            }
            _snake.Move();
        }
    }
}