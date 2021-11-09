using UnityEngine;

public class Fruit : MonoBehaviour, IEntity
{
    [SerializeField] int _points = 100;

    public delegate void FruitEaten(int points);
    public static FruitEaten OnFruitEaten;

    public void Interact()
    {
        OnFruitEaten?.Invoke( _points );
        Destroy( gameObject );
    }

    public void SetTile(Tile tile)
    {
        tile.Enter( this );
        transform.position = tile.transform.position;
    }
}