using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    public int width;
    public int height;
    public Node[,] nodes;

    public void Init(int width, int height)
    {
        this.width = width;
        this.height = height;

        nodes = new Node[width, height];

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {

                Walls walls = new Walls
                {
                    northWall = true,
                    eastWall = true,
                    southWall = true,
                    westWall = true
                };

                Node node = new Node();
                node.Init(x, y, walls);
                nodes[x, y] = node;
                node.position = new Vector3(x, 0, -y);
            }

        }
    }

    public void CreateNeighborsList()
    {
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                nodes[x, y].neighbors = GetNeighbors(x, y);
            }
        }
    }

    private List<Node> GetNeighbors(int x, int y)
    {
        List<Node> neigborsNodes = new List<Node>();

        if(IsWithinBounds(x, y - 1) && nodes[x, y - 1] != null && !nodes[x, y].walls.northWall)
        {
            neigborsNodes.Add(nodes[x, y -1]);
        }
        if(IsWithinBounds(x + 1, y) && nodes[x + 1, y] != null && !nodes[x, y].walls.eastWall)
        {
            neigborsNodes.Add(nodes[x + 1, y]);
        }
        if(IsWithinBounds(x, y + 1) && nodes[x, y + 1] != null && !nodes[x, y].walls.southWall)
        {
            neigborsNodes.Add(nodes[x, y + 1]);
        }
        if(IsWithinBounds(x - 1, y) && nodes[x - 1, y] != null && !nodes[x, y].walls.westWall)
        {
            neigborsNodes.Add(nodes[x - 1, y]);
        }

        return neigborsNodes;
    }

    public bool IsWithinBounds(int x, int y)
    {
        return (x >= 0 && x < width && y >= 0 && y < height);
    }
}
