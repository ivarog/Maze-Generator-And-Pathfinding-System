using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeView : MonoBehaviour
{
    [SerializeField] GameObject tile;

    public void Init(Node node)
    {
        if(tile != null)
        {
            gameObject.name = "[" + node.x + "," + node.y + "]";
            gameObject.transform.position = node.position;
        }
    }
}
