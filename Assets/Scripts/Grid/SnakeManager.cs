using UnityEngine;
using Hjelmqvist.Collections.Generic;
using UnityEngine.Events;

public class SnakeManager : MonoBehaviour
{
    [SerializeField] GridManager _grid;
    [SerializeField] FruitManager _fruitManager;
    [SerializeField] SnakePart _headPrefab;
    [SerializeField] SnakePart _tailPrefab;

    [Space( 10 )]
    [SerializeField, Min( 2 )] int _startTailSize = 2;
    [SerializeField] Vector2Int _startPosition;
    [SerializeField] Vector2Int _startDirection;

    [Space( 10 )]
    [SerializeField] SnakeInputSO _inputs;
    [SerializeField] float _timeBetweenMoves = 0.2f;
    float _timeSinceLastMove = 0;

    [Space( 20 )]
    public UnityEvent OnSnakeDeath;

    Vector2Int _currentDirection;
    Vector2Int _previousLastPosition;   
    LinkedList<SnakePart> _snake = new LinkedList<SnakePart>();

    public Vector2Int CurrentPosition => _snake.First.Value.Position;

    private void Awake()
    {
        // Instantiate a new instance of the SnakeInputs to not do changes to the ScriptableObjects.
        _inputs = Instantiate( _inputs );
        _inputs.SetReferences( _grid, _fruitManager, this );
    }

    private void Update()
    {
        _inputs.UpdateValues();
        _timeSinceLastMove += Time.deltaTime;
        if (_timeSinceLastMove >= _timeBetweenMoves)
        {
            _timeSinceLastMove -= _timeBetweenMoves;
            _currentDirection = _inputs.GetDirection();
            Move();
        }
    }

    private void AddPart(SnakePart prefab, Vector2Int position)
    {
        SnakePart part = Instantiate( prefab, transform );
        part.SetInteractionCallback( SnakePart_OnSnakeDeath );
        part.SetTile( _grid.GetTile( position ) );
        _snake.AddLast( part );
    }

    /// <summary>
    /// Will not change if direction is directly backwards
    /// </summary>
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
        var head = _snake.First;
        Vector2Int previousPosition = head.Value.Position;
        Tile nextHeadTile = _grid.GetTile( previousPosition + _currentDirection );

        // Move tail pieces to the previous piece old position
        var current = head.Next;
        while (current != null)
        {
            Vector2Int currentPosition = current.Value.Position;
            current.Value.SetTile( _grid.GetTile( previousPosition ) );

            previousPosition = currentPosition;
            current = current.Next;  
        }

        head.Value.SetTile( nextHeadTile );
    }

    public void Spawn()
    {
        _currentDirection = _startDirection;
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