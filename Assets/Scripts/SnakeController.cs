using UnityEngine;
using Hjelmqvist.Collections.Generic;

public class SnakeController : MonoBehaviour, IEntity
{
    [SerializeField] GridManager _grid;
    [SerializeField] SnakePart _headPrefab;
    [SerializeField] SnakePart _tailPrefab;

    [Header("Start settings"), Space(10)]
    [SerializeField, Min(2)] int _startTailSize = 1;
    [SerializeField] Vector2Int _startPosition;
    [SerializeField] Vector2Int _startDirection;

    Vector2Int _currentPosition;
    Vector2Int _currentDirection;

    LinkedList<SnakePart> _snake = new LinkedList<SnakePart>();

    private void Start()
    {
        _snake.Clear();
        AddPart( _headPrefab );
        for (int i = 0; i < _startTailSize; i++)
            AddPart( _tailPrefab );
    }

    private void AddPart(SnakePart prefab)
    {
        var lastNode = _snake.Last;
        SnakePart part = Instantiate( prefab );
        Tile tile = null;

        if (lastNode == null)
            tile = _grid.GetTile( _startPosition );
        else
        {

        }

        part.SetTile( tile );
        _snake.AddLast( part );
    }

    public void Move(Vector2Int direction)
    {
        // Can't move directly backwards
        if (direction == -_currentDirection)
            return;

        _currentPosition += direction;
        _currentDirection = direction;


    }

    public void Interact(IEntity other)
    {
        throw new System.NotImplementedException();
    }

    public void SetTile(Tile tile)
    {
        throw new System.NotImplementedException();
    }
}