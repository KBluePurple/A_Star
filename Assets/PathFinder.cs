using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class PathFinder : MonoBehaviour
    {
        public static Tile StartTile;
        public static Tile EndTile;
        
        public float delay = 0.1f;
        
        public static PathFinder Instance;

        private Tile[,] _tileMap;
        private List<Tile> _openList = new List<Tile>();

        private void Awake()
        {
            Instance = this;
        }
        
        public void SetTiles(Tile[,] tiles)
        {
            _tileMap = tiles;
        }
        
        public void FindPath(Vector2Int start, Vector2Int end)
        {
            StartTile = _tileMap[start.x, start.y];
            EndTile = _tileMap[end.x, end.y];
            
            StartCoroutine(FindPath());
        }

        private IEnumerator FindPath()
        {
            while (true)
            {
                StartTile.isVisited = true;
                StartTile.isLocked = true;
                
                Tile[] tilesAround = GetTilesAround(StartTile);
                foreach (var tile in tilesAround)
                {
                    if (tile.isVisited && tile.h < StartTile.h + 1 || tile.isLocked || !tile.isWalkable)
                        continue;
                    
                    tile.isVisited = true;
                    tile.parent = StartTile;

                    tile.h = StartTile.h + 1;
                    
                    _openList.Add(tile);
                    
                    if (tile == EndTile)
                    {
                        EndTile.parent = StartTile;
                        
                        var currentTile = EndTile;
                        while (currentTile.parent != null)
                        {
                            currentTile.GetComponent<SpriteRenderer>().color = Color.cyan;
                            currentTile = currentTile.parent;
                            yield return new WaitForSeconds(delay);
                        }
                        yield break;
                    }
                    yield return new WaitForSeconds(delay);
                }
                
                StartTile = _openList.Where(x => !x.isLocked).OrderBy(x => x.F).First();
                yield return new WaitForSeconds(delay);
            }
        }

        private Tile[] GetTilesAround(Tile startTile)
        {
            List<Tile> tilesAround = new List<Tile>();
            
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                    {
                        continue;
                    }
                    
                    Vector2Int tilePos = new Vector2Int(startTile.position.x + x, startTile.position.y + y);
                    
                    if (tilePos.x < 0 || tilePos.x >= _tileMap.GetLength(0) || tilePos.y < 0 || tilePos.y >= _tileMap.GetLength(1))
                    {
                        continue;
                    }
                    
                    tilesAround.Add(_tileMap[tilePos.x, tilePos.y]);
                }
            }

            return tilesAround.ToArray();
        }
    }
}