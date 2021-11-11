using UnityEngine;

public class SnakePart : Entity<SnakePart>
{
    [SerializeField] bool _isHead;

    public override void SetTile(Tile tile)
    {
        if (_isHead)
            tile.Interact();
        base.SetTile( tile );
    }
}