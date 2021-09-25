using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeView : MonoBehaviour
{
    [SerializeField] GameObject tile;
    [SerializeField] GameObject northWall;
    [SerializeField] GameObject eastWall;
    [SerializeField] GameObject southWall;
    [SerializeField] GameObject westWall;

    public void Init(Node node)
    {
        if(tile != null)
        {
            gameObject.name = "[" + node.x + "," + node.y + "]";
            gameObject.transform.position = node.position;
            HandleWalls(node.walls);
        }
    }

    private void HandleWalls(Walls walls)
    {
        northWall.SetActive(walls.northWall);
        eastWall.SetActive(walls.eastWall);
        southWall.SetActive(walls.southWall);
        westWall.SetActive(walls.westWall);
    }
}
