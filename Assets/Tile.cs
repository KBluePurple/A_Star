using System;
using DefaultNamespace;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2Int position;
    public int h = 1;
    public int temp = 0;
    private int g => Mathf.Abs(PathFinder.EndTile.position.x - position.x) + Mathf.Abs(PathFinder.EndTile.position.y - position.y);
    public int F => g + h;
    public Tile parent;

    public bool isVisited = false;
    public bool isLocked = false;
    
    public bool isWalkable = true;

    private void Update()
    {
        if (isLocked) return;
        
        if (PathFinder.StartTile == this)
        {
            GetComponent<SpriteRenderer>().color = Color.green;
        }
        else if (PathFinder.EndTile == this)
        {
            GetComponent<SpriteRenderer>().color = Color.blue;
        }
        else if (isLocked)
        {
            GetComponent<SpriteRenderer>().color = Color.green;
        }
        else if (isVisited)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else if (isWalkable)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.black;
        }

        temp = PathFinder.EndTile.position.x - position.x + PathFinder.EndTile.position.y - position.y;
    }

    public Tile previousTile;
}