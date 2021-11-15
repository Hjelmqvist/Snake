using UnityEngine;
using UnityEngine.Events;

public class FruitManager : MonoBehaviour
{
    [SerializeField] GridManager _grid;
    [SerializeField] Fruit[] _fruitPrefabs;

    public Tile FruitTile { get; private set; }

    public UnityEvent<int> OnFruitEaten;

    public void SpawnFruit()
    {
        if (_fruitPrefabs.Length == 0)
            return;

        Tile tile = _grid.GetRandomEmptyTile();
        FruitTile = tile;
        Fruit fruit = Instantiate( _fruitPrefabs[Random.Range( 0, _fruitPrefabs.Length )] );
        fruit.SetInteractionCallback( Fruit_OnFruitEaten );
        fruit.SetTile( tile );
    }

    public void Fruit_OnFruitEaten(Fruit fruit)
    {
        OnFruitEaten?.Invoke( fruit.Points );
        Destroy( fruit.gameObject );
        SpawnFruit();
    }
}