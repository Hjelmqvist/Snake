using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] SnakeController _snake;
    [SerializeField] MovementKey[] _inputs;

    [System.Serializable]
    struct MovementKey
    {
        public KeyCode keyCode;
        public Vector2Int direction;
    }

    private void Update()
    {
        foreach (MovementKey input in _inputs)
        {
            if (Input.GetKeyDown( input.keyCode ))
                _snake.Move( input.direction );
        }
    }
}