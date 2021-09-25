using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] int width;
    [SerializeField] int height;
    [SerializeField] GraphView graphView;

    Graph graph = new Graph();
    MazeGenerator mazeGenerator = new MazeGenerator();

    private void Start() 
    {
        graph.Init(width, height);
        mazeGenerator.Init(graph);
        mazeGenerator.HuntAndKillAlgorithm();
        graphView?.Init(graph);
    }

    // private void Update() 
    // {
    //     if(Input.GetMouseButtonUp(0))
    //     {
    //     }    
    // }
}
