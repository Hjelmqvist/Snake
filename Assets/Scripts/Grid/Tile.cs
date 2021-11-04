using UnityEngine;
using Hjelmqvist.Pathfinding;

public class Tile : MonoBehaviour, IPathable
{
    IEntity _currentEntity;

    public Vector2Int Position { get; private set; }

    public bool IsWalkable() => true;

    public void Enter(IEntity entity)
    {
        if (_currentEntity != null)
            _currentEntity.Interact( entity );
        _currentEntity = entity;
    }

    public void Exit(IEntity entity)
    {
        if (_currentEntity == entity)
            _currentEntity = null;
    }

    public void SetPosition(Vector2Int position)
    {
        Position = position;
    }
}