using UnityEngine;
using UnityEngine.Events;

public class FruitManager : MonoBehaviour
{
    [SerializeField] GridManager _grid;
    [SerializeField] Fruit[] _fruitPrefabs;

    [Space( 20 )]
    public UnityEvent<int> OnFruitEaten;

    public Tile FruitTile { get; private set; }

    public void SpawnFruit()
    {
        if (_fruitPrefabs.Length == 0)
            return;

        FruitTile = _grid.GetRandomEmptyTile();
        Fruit fruit = Instantiate( _fruitPrefabs[Random.Range( 0, _fruitPrefabs.Length )] );
        fruit.SetInteractionCallback( Fruit_OnFruitEaten );
        fruit.SetTile( FruitTile );
    }

    public void Fruit_OnFruitEaten(Fruit fruit)
    {
        OnFruitEaten.Invoke( fruit.Points );
        Destroy( fruit.gameObject );
        SpawnFruit();
    }
}