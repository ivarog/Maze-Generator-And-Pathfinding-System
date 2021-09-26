using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder
{
    Node startNode;
    Node goalNode;
    Graph graph;

    Queue<Node> stack;
    List<Node> visitedNodes;
    List<Node> solutionNodes;
    GraphView graphView;
    Material solutionMaterial;
    Material defaultMaterial;

    public bool isComplete;

    public void Init(Graph graph, Node start, Node goal, GraphView graphView, Material solutionMaterial, Material defaultMaterial)
    {
        this.startNode = start;
        this.goalNode = goal;
        this.graph = graph;
        this.graphView = graphView;
        this.solutionMaterial = solutionMaterial;
        this.defaultMaterial = defaultMaterial;

        if(solutionNodes != null && solutionNodes.Count > 0)
        {
            graphView.ColorNodes(solutionNodes, defaultMaterial);
        }

        stack = new Queue<Node>();
        stack.Enqueue(start);
        visitedNodes = new List<Node>();
        solutionNodes = new List<Node>();
        isComplete = false;

        for(int x = 0; x < graph.width; x++)
        {
            for(int y = 0; y < graph.height; y++)
            {
                graph.nodes[x, y].Reset();
            }
        }
    }

    public List<Node> GeneratePath()
    {
        while(!isComplete)
        {
            if(stack.Count > 0)
            {
                // Debug.Log("Starting search");
                Node currentNode = stack.Dequeue();

                if(!visitedNodes.Contains(currentNode))
                {
                    visitedNodes.Add(currentNode);
                }

                GetUnvisitedNeighbors(currentNode);
                // Debug.Log(stack.Count);

                if(stack.Contains(goalNode))
                {
                    solutionNodes = GetSolutionNodes(goalNode);
                    isComplete = true;
                }
            }
            else
            {
                isComplete = true;
            }
        }

        graphView.ColorNodes(solutionNodes, solutionMaterial);
        return solutionNodes;
    }

    private void GetUnvisitedNeighbors(Node node)
    {
        for(int i = 0; i < node.neighbors.Count; i++)
        {
            if(!visitedNodes.Contains(node.neighbors[i]) && !stack.Contains(node.neighbors[i]))
            {
                node.neighbors[i].previousNode = node;
                stack.Enqueue(node.neighbors[i]);
            }
        }
    }

    private List<Node> GetSolutionNodes(Node goalNode)
    {
        List<Node> solution = new List<Node>();
        
        if(goalNode == null) return solution;

        solution.Add(goalNode);

        Node currentNode = goalNode.previousNode;

        while(currentNode != null)
        {
            solution.Insert(0, currentNode);
            currentNode = currentNode.previousNode;
        }

        return solution;
    }

}
