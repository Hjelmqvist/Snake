using UnityEngine;

public class SnakePart : MonoBehaviour, IEntity
{
    Transform _part;
    Tile _currentTile;

    public SnakePart(Transform part, Tile tile)
    {
        _part = part;
        _currentTile = tile;
    }

    public void Interact(IEntity other)
    {
        // Lost
    }

    public void SetTile(Tile tile)
    {
        _currentTile.Exit(this);
        tile.Enter( this );
        _part.position = tile.transform.position;
        _currentTile = tile;
    }
}