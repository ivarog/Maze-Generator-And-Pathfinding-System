using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
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
}
