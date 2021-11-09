using UnityEngine;
using UnityEngine.Events;

public class GridManager : MonoBehaviour
{
    [SerializeField] Tile _tilePrefab;
    [SerializeField] Fruit[] _fruitPrefabs;

    [SerializeField] int _xSize = 10;
    [SerializeField] int _ySize = 10;

    Tile[,] _grid = null;
    GameObject _gridParent;
    int _score;

    const string GRID_PARENT_NAME = "Grid Parent";

    public UnityEvent<ScoreArgs> OnPointsAdded;

    private void Awake()
    {
        SetupGrid();
        SetCameraPosition();
    }

    private void Start()
    {
        PlaceFruit();
    }

    private void SetupGrid()
    {
        if (_gridParent != null)
            Destroy( _gridParent );
        _gridParent = new GameObject( GRID_PARENT_NAME );

        _grid = new Tile[_xSize, _ySize];
        for (int x = 0; x < _xSize; x++)
        {
            for (int y = 0; y < _ySize; y++)
            {
                Tile tile = Instantiate( _tilePrefab, new Vector3( x + 1, y + 1 ), Quaternion.identity, _gridParent.transform );
                tile.SetPosition( new Vector2Int( x, y ) );
                _grid[x, y] = tile;
            }
        }
    }

    private void PlaceFruit()
    {
        Tile tile = GetRandomEmptyTile();
        Fruit fruit = Instantiate( _fruitPrefabs[0] );
        fruit.SetTile( tile );
    }

    private Tile GetRandomEmptyTile()
    {
        Tile tile = null;

        do
        {
            int x = Random.Range( 0, _xSize );
            int y = Random.Range( 0, _ySize );
            Vector2Int randomPos = new Vector2Int( x, y );
            tile = GetTile( randomPos );
        } while (!tile.IsEmpty);

        return tile;
    }

    private void SetCameraPosition()
    {
        Camera camera = Camera.main;
        Vector3 pos = camera.transform.position;

        // Set position to the middle of the board
        pos.x = (_xSize + 1) / 2f;
        pos.y = (_ySize + 1) / 2f;

        camera.transform.position = pos;

        // Update orthographic size to fit all the tiles
        // :thinking:
    }

    public Tile GetTile(Vector2Int position)
    {
        // Wrap around to the other side
        if (position.x < 0)
            position.x += _xSize;
        else if (position.x >= _xSize)
            position.x -= _xSize;

        if (position.y < 0)
            position.y += _ySize;
        else if (position.y >= _ySize)
            position.y -= _ySize;

        return _grid[position.x, position.y];
    }

    private void OnEnable() => Fruit.OnFruitEaten += OnFruitEaten;

    private void OnDisable() => Fruit.OnFruitEaten -= OnFruitEaten;

    private void OnFruitEaten(int points)
    {
        PlaceFruit();
        _score += points;
        OnPointsAdded.Invoke( new ScoreArgs( points, _score ) );
    }
}

public readonly struct ScoreArgs
{
    public readonly int PointsAdded { get; }
    public readonly int Score { get; }

    public ScoreArgs(int pointsAdded, int score)
    {
        PointsAdded = pointsAdded;
        Score = score;
    }
}