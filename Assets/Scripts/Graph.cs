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


        for(int x = 0; x < this.width; x++)
        {
            for(int y = 0; y < this.height; y++)
            {
                Node node = new Node();
                node.Init(x, y);
                nodes[x, y] = node;
                node.position = new Vector3(x, 0, y);
            }

        }

    }
}
