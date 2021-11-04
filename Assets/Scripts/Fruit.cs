using UnityEngine;

public class Fruit : MonoBehaviour, IEntity
{
    [SerializeField] int _points = 100;

    public void Interact(IEntity other)
    {
        // Add points to score
    }

    public void SetTile(Tile tile)
    {
        
    }
}