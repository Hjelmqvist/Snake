using UnityEngine;

public class SnakePart : MonoBehaviour, IEntity
{
    Tile _currentTile;

    public Vector2Int Position => _currentTile != null ? _currentTile.Position : Vector2Int.zero;

    public delegate void SnakeDied();
    public static SnakeDied OnSnakeDied;

    public void Interact()
    {
        OnSnakeDied?.Invoke();
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