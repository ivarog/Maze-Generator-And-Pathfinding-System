using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphView : MonoBehaviour
{
    [SerializeField] GameObject nodeViewPrefab;

    private NodeView[,] nodeViews;

    public void Init(Graph graph)
    {
        nodeViews = new NodeView[graph.width, graph.height];
        foreach(Node node in graph.nodes)
        {
            GameObject instance = Instantiate(nodeViewPrefab, Vector3.zero, Quaternion.identity);
            NodeView nodeView = instance.GetComponent<NodeView>();
            nodeViews[node.x, node.y] = nodeView;
            nodeView?.Init(node);
        }
    }

    public void ColorNodes(List<Node> nodes, Material material)
    {
        foreach(Node node in nodes)
        {
            NodeView nodeView = nodeViews[node.x, node.y];
            nodeView.ColorNode(material);
        }
    }
}
