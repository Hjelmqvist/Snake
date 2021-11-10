using UnityEngine;
using Hjelmqvist.Pathfinding;

public class Tile : MonoBehaviour, IPathable
{
    IEntity _currentEntity;

    public Vector2Int Position { get; private set; }
    public bool IsEmpty => _currentEntity == null;
    public bool IsWalkable() => _currentEntity == null || _currentEntity.IsWalkable; // Used for pathfinding

    public void Enter(IEntity entity)
    {
        _currentEntity = entity;
    }

    public void Exit(IEntity entity)
    {
        if (_currentEntity == entity)
            _currentEntity = null;
    }

    public void Interact()
    {
        if (_currentEntity != null)
            _currentEntity.Interact( );
    }

    public void SetPosition(Vector2Int position)
    {
        Position = position;
    }
}