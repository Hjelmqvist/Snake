using System;
using UnityEngine;

public interface IEntity
{
    bool IsWalkable { get; }
    Vector2Int Position { get; }
    void Interact();
    void SetTile(Tile tile);
}

public abstract class Entity<T> : MonoBehaviour, IEntity where T : Entity<T>
{
    [SerializeField] bool _isWalkable = true;
    protected Tile _currentTile;

    Action<T> OnInteraction;

    public bool IsWalkable => _isWalkable;
    public Vector2Int Position => _currentTile != null ? _currentTile.Position : Vector2Int.zero;

    public void Interact() => OnInteraction?.Invoke( this as T );

    public void SetInteractionCallback(Action<T> interactionCallback) => OnInteraction = interactionCallback;

    public virtual void SetTile(Tile tile)
    {
        if (_currentTile != null)
            _currentTile.Exit( this );
        tile.Enter( this );
        _currentTile = tile;

        transform.position = tile.transform.position;
    }
}