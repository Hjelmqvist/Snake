using Hjelmqvist.Pathfinding;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GridManager : MonoBehaviour
{
    [SerializeField] Tile _tilePrefab;
    [SerializeField] int _xSize = 10;
    [SerializeField] int _ySize = 10;
    [SerializeField] Vector2Int[] _connectionDirections = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

    Tile[,] _grid = null;
    GameObject _gridParent;

    const string GRID_PARENT_NAME = "Grid Parent";

    public UnityEvent OnGridCreated;

    private void Awake()
    {
        CreateGrid();
        SetupConnections();
        SetCameraPosition();
        OnGridCreated.Invoke();
    }

    private void Update()
    {
        UnityEngine.Profiling.Profiler.BeginSample( "AStar bottom left -> top right" );
        Pathfinding.AStar.TryGetPath( _grid[0, 0], _grid[_xSize - 1, _ySize - 1], out _ );
        UnityEngine.Profiling.Profiler.EndSample();
    }

    private void CreateGrid()
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

    private void SetupConnections()
    {
        for (int x = 0; x < _xSize; x++)
        {
            for (int y = 0; y < _ySize; y++)
            {
                List<IPathable> connections = new List<IPathable>();
                foreach (Vector2Int dir in _connectionDirections)
                    connections.Add( GetTile( new Vector2Int( x + dir.x, y + dir.y ) ) );
                _grid[x, y].SetConnections( connections );
            }
        }
    }

    public Tile GetRandomEmptyTile()
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

        // TODO: Update orthographic size to fit all the tiles :thinking:
    }

    public Tile GetTile(Vector2Int position)
    {
        // Screen wrapping
        int x = (position.x + _xSize) % _xSize;
        int y = (position.y + _ySize) % _ySize;
        return _grid[x, y];
    }
}