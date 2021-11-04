using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Tile _tilePrefab;
    [SerializeField] int _xSize = 10;
    [SerializeField] int _ySize = 10;

    Tile[,] _grid = null;
    GameObject _gridParent;

    const string GRID_PARENT_NAME = "Grid Parent";

    private void Awake()
    {
        SetupGrid();
        SetCameraPosition();
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
                Tile tile = Instantiate( _tilePrefab, new Vector3( x + 1, 0, y + 1 ), Quaternion.identity, _gridParent.transform );
                tile.SetPosition( new Vector2Int( x, y ) );
                _grid[x, y] = tile;
            }
        }
    }

    private void SetCameraPosition()
    {
        Camera camera = Camera.main;
        Vector3 pos = camera.transform.position;
        pos.x = (_xSize + 1) / 2f;
        pos.z = (_ySize + 1) / 2f;
        camera.transform.position = pos;

        // ortographic size
    }

    public Tile GetTile(Vector2Int position)
    {
        int x = position.x % _xSize;
        int y = position.y % _ySize;
        return _grid[x, y];
    }
}