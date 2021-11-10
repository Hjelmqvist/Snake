public interface IEntity
{
    bool IsWalkable { get; }
    void Interact();
    void SetTile(Tile tile);
}