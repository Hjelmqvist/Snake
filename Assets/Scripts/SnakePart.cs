using System;
using UnityEngine;

public class SnakePart : MonoBehaviour, IEntity
{
    Tile _currentTile;

    public bool IsWalkable => false; // Used for pathfinding
    public Vector2Int Position => _currentTile != null ? _currentTile.Position : Vector2Int.zero;

    Action OnSnakeDeath;

    public void Interact()
    {
        OnSnakeDeath?.Invoke();
    }

    public void SetDeathCallback(Action snakeDiedCallback)
    {
        OnSnakeDeath = snakeDiedCallback;
    }

    public void SetTile(Tile tile)
    {
        if (_currentTile != null)
            _currentTile.Exit(this);
        tile.Enter( this );
        transform.position = tile.transform.position;
        _currentTile = tile;
    }
}