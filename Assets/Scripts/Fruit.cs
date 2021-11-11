using System;
using UnityEngine;

public class Fruit : MonoBehaviour, IEntity
{
    [SerializeField] int _points = 100;

    public bool IsWalkable => true; // Used for pathfinding

    Action<int> OnFruitEaten;

    public void Interact()
    {
        OnFruitEaten?.Invoke( _points );
        Destroy( gameObject );
    }

    public void SetEatenCallback(Action<int> onEatenCallback)
    {
        OnFruitEaten = onEatenCallback;
    }

    public void SetTile(Tile tile)
    {
        tile.Enter( this );
        transform.position = tile.transform.position;
    }
}