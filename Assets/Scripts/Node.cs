using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public int x = -1;
    public int y = -1;

    public List<Node> neighbors = new List<Node>();
    public Vector3 position;
    
    public void Init(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}
