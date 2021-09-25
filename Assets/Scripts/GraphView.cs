using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphView : MonoBehaviour
{
    [SerializeField] GameObject nodeViewPrefab;

    public void Init(Graph graph)
    {
        foreach(Node node in graph.nodes)
        {
            GameObject instance = Instantiate(nodeViewPrefab, Vector3.zero, Quaternion.identity);
            NodeView nodeView = instance.GetComponent<NodeView>();
            nodeView?.Init(node);
        }
    }
}
