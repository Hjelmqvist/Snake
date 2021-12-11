using UnityEngine;

public abstract class InputSO : ScriptableObject
{
    [SerializeField] protected Vector2Int _currentDirection = Vector2Int.down;
    protected GridManager _grid;
    protected FruitManager _fruitManager;
    protected SnakeManager _snake;

    public void SetReferences(GridManager grid, FruitManager fruits, SnakeManager snake)
    {
        _grid = grid;
        _fruitManager = fruits;
        _snake = snake;
    }

    public virtual Vector2Int GetDirection() => _currentDirection;
    public abstract void UpdateValues();
}
