using UnityEngine;

[CreateAssetMenu(fileName = "New Player Input", menuName = "Snake Input/Player Input")]
public class PlayerInputSO : InputSO
{
    [SerializeField] MovementKey[] _inputs = 
    {
        new MovementKey(KeyCode.W, Vector2Int.up),
        new MovementKey(KeyCode.S, Vector2Int.down),
        new MovementKey(KeyCode.A, Vector2Int.left),
        new MovementKey(KeyCode.D, Vector2Int.right)
    };

    [System.Serializable]
    struct MovementKey
    {
        public KeyCode keyCode;
        public Vector2Int direction;

        public MovementKey(KeyCode key, Vector2Int dir)
        {
            keyCode = key;
            direction = dir;
        }
    }

    public override void UpdateValues()
    {
        foreach (MovementKey input in _inputs)
        {
            if (Input.GetKeyDown( input.keyCode ))
            {
                _currentDirection = input.direction;
                return;
            }
        }
    }
}