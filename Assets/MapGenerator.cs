using DefaultNamespace;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public Vector2Int endPosition;

    private Tile[,] _map;
    
    [SerializeField] private int width;
    [SerializeField] private int height;
    
    [SerializeField] private GameObject tilePrefab;

    private void Start()
    {
        _map = new Tile[width, height];
        GenerateMap();
    }
    
    private void GenerateMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject tile = Instantiate(tilePrefab, transform.position + new Vector3(x, y, 0), Quaternion.identity);
                tile.transform.parent = transform;
                _map[x, y] = tile.GetComponent<Tile>();
                _map[x, y].position = new Vector2Int(x, y);
            }
        }
        
        PathFinder.Instance.SetTiles(_map);
        PathFinder.Instance.FindPath(new Vector2Int(0, 0), endPosition);
    }
}
