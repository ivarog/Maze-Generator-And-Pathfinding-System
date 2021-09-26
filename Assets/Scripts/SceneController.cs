using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] int width;
    [SerializeField] int height;
    [SerializeField] GraphView graphView;
    [SerializeField] int startX;
    [SerializeField] int startY;
    [SerializeField] int goalX;
    [SerializeField] int goalY;
    [SerializeField] Material solutionMaterial;
    [SerializeField] Material defaultMaterial;
    [SerializeField] Agent agent;

    Graph graph = new Graph();
    MazeGenerator mazeGenerator = new MazeGenerator();
    Pathfinder pathfinder = new Pathfinder();
    Node startNode;
    Node goalNode;

    private void Start() 
    {
        graph.Init(width, height);
        mazeGenerator.Init(graph);
        mazeGenerator.HuntAndKillAlgorithm();
        graph.CreateNeighborsList();
        graphView?.Init(graph);

        if(graph.IsWithinBounds(startX, startY) && graph.IsWithinBounds(goalX, goalY))
        {
            startNode = graph.nodes[startX, startY];
            goalNode = graph.nodes[goalX, goalY];
            pathfinder.Init(graph, startNode, goalNode, graphView, solutionMaterial, defaultMaterial);
            List<Node> path = pathfinder.GeneratePath();
            agent.Walk(path);
            startNode = goalNode;
        }

    }

    private void Update() 
    {
        DetectClick();
    }

    private void DetectClick()
    {
        if(Input.GetMouseButtonDown(0) && !agent.IsWalking)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if(Physics.Raycast (ray, out hit))
            {
                if(hit.transform.CompareTag("Node"))
                {
                    NodeView nodeView = hit.transform.parent.GetComponent<NodeView>(); 
                    Node goalNode = nodeView.node;
                    pathfinder.Init(graph, startNode, goalNode, graphView, solutionMaterial, defaultMaterial);
                    List<Node> path = pathfinder.GeneratePath();
                    startNode = goalNode;
                    agent.Walk(path);
                }
            }
        }
    }

}
