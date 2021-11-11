using System;
using UnityEngine;

public interface IEntity
{
    bool IsWalkable { get; }
    void Interact();
    void SetTile(Tile tile);
}

// TODO: Abstract class for entities instead of interface
//public abstract class Entity : MonoBehaviour
//{
//    Tile _currentTile;
//    Action<Entity> OnInteraction;

//    public abstract bool IsWalkable { get; }

//    public void Interact()
//    {
//        OnInteraction?.Invoke( this );
//    }
    
//    public void SetInteractionCallback(Action<Entity> interactionCallback)
//    {
//        OnInteraction = interactionCallback;
//    }

//    public virtual void SetTile(Tile tile)
//    {
//        //if (_currentTile != null)
//        //    _currentTile.Exit( this );
//        //tile.Enter( this );
//        _currentTile = tile;

//        transform.position = tile.transform.position;
//    }
//}

//public class FruitEntity : Entity
//{
//    public override bool IsWalkable => true;
//}