using UnityEngine;
using Hjelmqvist.Collections.Generic;

public class SnakeController : MonoBehaviour
{
    [SerializeField] GridManager _grid;
    [SerializeField] SnakePart _headPrefab;
    [SerializeField] SnakePart _tailPrefab;

    [Space(10)]
    [SerializeField, Min(2)] int _startTailSize = 1;
    [SerializeField] Vector2Int _startPosition;
    [SerializeField] Vector2Int _startDirection;

    Vector2Int _lastDirection;
    Vector2Int _previousLastPosition;

    LinkedList<SnakePart> _snake = new LinkedList<SnakePart>();

    public void Spawn()
    {
        _snake.Clear();
        AddPart( _headPrefab, _startPosition );
        for (int i = 0; i < _startTailSize; i++)
            AddPart( _tailPrefab, _startPosition - _startDirection * (i + 1) );
    }

    private void AddPart(SnakePart prefab, Vector2Int position)
    {
        SnakePart part = Instantiate( prefab );
        Tile tile = _grid.GetTile( position);

        part.SetTile( tile );
        _snake.AddLast( part );
    }

    public void Move(Vector2Int direction)
    {
        // Can't move directly backwards
        if (direction == -_lastDirection)
            return;

        // Get the previous position of the head and the tile to move the head to
        var current = _snake.First;
        Vector2Int previousPosition = current.Value.Position;
        Tile movedToTile = _grid.GetTile( previousPosition + direction );
        _previousLastPosition = _snake.Last.Value.Position;

        // Move tail pieces to the previous piece old position
        current = current.Next;
        while (current != null)
        {
            Vector2Int temp = current.Value.Position;
            current.Value.SetTile( _grid.GetTile( previousPosition ) );
            previousPosition = temp;
            current = current.Next;
        }

        // Interact with tile before moving head to it
        movedToTile.Interact();
        _snake.First.Value.SetTile( movedToTile );
        _lastDirection = direction;
    }

    private void OnEnable()
    {
        Fruit.OnFruitEaten += OnFruitEaten;
        SnakePart.OnSnakeDied += OnSnakeDied;
    }


    private void OnDisable()
    {
        Fruit.OnFruitEaten -= OnFruitEaten;
        SnakePart.OnSnakeDied -= OnSnakeDied;
    }

    void OnFruitEaten(int points)
    {
        AddPart( _tailPrefab, _previousLastPosition );
    }

    void OnSnakeDied()
    {
        // Stop movement
        Debug.Log( "Snek dieded!" );
    }
}