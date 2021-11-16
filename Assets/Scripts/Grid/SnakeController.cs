using UnityEngine;
using Hjelmqvist.Collections.Generic;
using UnityEngine.Events;

public class SnakeController : MonoBehaviour
{
    [SerializeField] GridManager _grid;
    [SerializeField] SnakePart _headPrefab;
    [SerializeField] SnakePart _tailPrefab;

    [Space( 10 )]
    [SerializeField, Min( 2 )] int _startTailSize = 2;
    [SerializeField] Vector2Int _startPosition;
    [SerializeField] Vector2Int _startDirection;

    Vector2Int _currentDirection;
    Vector2Int _previousLastPosition;   

    LinkedList<SnakePart> _snake = new LinkedList<SnakePart>();

    public Vector2Int CurrentPosition => _snake.First.Value.Position;

    public UnityEvent OnSnakeDeath;

    private void Awake()
    {
        _currentDirection = _startDirection;
    }

    private void AddPart(SnakePart prefab, Vector2Int position)
    {
        SnakePart part = Instantiate( prefab, transform );
        part.SetInteractionCallback( SnakePart_OnSnakeDeath );
        part.SetTile( _grid.GetTile( position ) );
        _snake.AddLast( part );
    }

    public void ChangeDirection(Vector2Int direction)
    {
        var headNode = _snake.First;
        Vector2Int directionToSecond = headNode.Next.Value.Position - headNode.Value.Position;
        if (direction == directionToSecond)
            return;
        _currentDirection = direction;
    }

    public void Move()
    {
        _previousLastPosition = _snake.Last.Value.Position;

        // Get the previous position of the head and the tile to move the head to
        var current = _snake.First;
        Vector2Int previousPosition = current.Value.Position;
        Tile nextHeadTile = _grid.GetTile( previousPosition + _currentDirection );

        // Move tail pieces to the previous piece old position
        current = current.Next;
        while (current != null)
        {
            Vector2Int currentPosition = current.Value.Position;
            current.Value.SetTile( _grid.GetTile( previousPosition ) );

            current = current.Next;
            previousPosition = currentPosition;
        }

        _snake.First.Value.SetTile( nextHeadTile );
    }

    public void Spawn()
    {
        _snake.Clear();
        AddPart( _headPrefab, _startPosition );
        for (int i = 0; i < _startTailSize; i++)
            AddPart( _tailPrefab, _startPosition - _startDirection * (i + 1) );
    }

    public void AddTailPiece()
    {
        AddPart( _tailPrefab, _previousLastPosition );
    }

    private void SnakePart_OnSnakeDeath(SnakePart part)
    {
        // Stop movement
        enabled = false;
        OnSnakeDeath.Invoke();
    }
}