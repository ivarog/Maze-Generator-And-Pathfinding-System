using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls
{
    public bool northWall = false;
    public bool westWall = false;
    public bool eastWall = false;
    public bool southWall = false;
}

public class Node
{
    public int x = -1;
    public int y = -1;

    public List<Node> neighbors = new List<Node>();
    public Vector3 position;
    public Walls walls = new Walls();
    public Node previousNode;
    
    public void Init(int x, int y, Walls walls)
    {
        this.x = x;
        this.y = y;
        this.walls = walls;
    }

    public void SetNorthWall(bool state)
    {
        if(walls != null)
        {
            walls.northWall = state;
        }
    }

    public void SetEastWall(bool state)
    {
        if(walls != null)
        {
            walls.eastWall = state;
        }
    }

    public void SetSouthWall(bool state)
    {
        if(walls != null)
        {
            walls.southWall = state;
        }
    }

    public void SetWestWall(bool state)
    {
        if(walls != null)
        {
            walls.westWall = state;
        }
    }

    public void Reset()
    {
        previousNode = null;
    }
}
