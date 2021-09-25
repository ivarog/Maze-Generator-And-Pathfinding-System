using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] int width;
    [SerializeField] int height;
    [SerializeField] GraphView graphView;

    Graph graph = new Graph();

    private void Start() 
    {
        graph.Init(width, height);
        graphView.Init(graph);
    }
}
