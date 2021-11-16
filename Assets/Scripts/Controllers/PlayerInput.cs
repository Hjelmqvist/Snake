using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] SnakeManager _snake;
    [SerializeField] MovementKey[] _inputs;

    [Space( 10 )]
    [SerializeField] float _timeBetweenMoves = 0.2f;
    float _timeSinceLastMove = 0;

    [System.Serializable]
    struct MovementKey
    {
        public KeyCode keyCode;
        public Vector2Int direction;
    }

    private void Update()
    {
        UpdateDirection();
        TryMoveSnake();
    }

    private void UpdateDirection()
    {
        foreach (MovementKey input in _inputs)
        {
            if (Input.GetKeyDown( input.keyCode ))
                _snake.ChangeDirection( input.direction );
        }
    }

    private void TryMoveSnake()
    {
        _timeSinceLastMove += Time.deltaTime;
        if (_timeSinceLastMove >= _timeBetweenMoves)
        {
            _timeSinceLastMove -= _timeBetweenMoves;
            _snake.Move();
        }
    }
}