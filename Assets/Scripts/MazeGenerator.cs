using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    private Graph graph;
    private int width;
    private int height;
    private int nx;
    private int ny;
    private NodeInfo[,] nodesInfo;
    bool allCellsVisited = false;

    private struct NodeInfo
    {
        public Node node;
        public bool visited;
    }

    public enum Directions
    {
        North = 1,
        East = 2,
        South = 3,
        West = 4
    }

    public void Init(Graph graph)
    {
        this.graph = graph;
        this.width = graph.width;
        this.height = graph.height;
        nx = 0;
        ny = 0;

        nodesInfo = new NodeInfo[width, height];

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                nodesInfo[x, y] = new NodeInfo
                {
                    node = graph.nodes[x, y],
                    visited = false
                };
            }
        }

        allCellsVisited = false;
    }

    public void HuntAndKillAlgorithm()
    {
        while(!allCellsVisited)
        {
            Walk();
            Hunt();
        }

        GenerateEntranceAndExit();
    }

    public void GenerateEntranceAndExit()
    {
        int exitX = 0, exitY = 0;

        nodesInfo[0, 0].node.SetWestWall(false);

        int horizontalExit = Random.Range(0, 2);

        if(horizontalExit == 0)
        {
            exitY = Random.Range(0, 2) * (height - 1);
            exitX = Random.Range(0, width);
        }
        else if(horizontalExit == 1)
        {
            exitY = Random.Range(0, height);
            exitX = Random.Range(0, 2) * (width - 1);
        }

        if(exitX == 0) nodesInfo[0, exitY].node.SetWestWall(false);
        else if(exitX == width - 1) nodesInfo[width - 1, exitY].node.SetEastWall(false);
        else if(exitY == 0) nodesInfo[exitX, 0].node.SetNorthWall(false);
        else if(exitY == height - 1) nodesInfo[exitX, height -1].node.SetSouthWall(false);

        Debug.Log(exitX);
        Debug.Log(exitY);

    }

    public void Walk()
    { 
        while(FindWalkablePath(nx, ny))
        {
            nodesInfo[nx, ny].visited = true;

            int newDirection = Random.Range(1, 5);

            if(newDirection == (int)Directions.North && IsWithinBounds(nx, ny - 1) && !CellVisited(nx, ny - 1)) 
            {
                nodesInfo[nx, ny].node.SetNorthWall(false);
                nodesInfo[nx, ny - 1].node.SetSouthWall(false);
                ny--;
            }
            else if(newDirection == (int)Directions.East && IsWithinBounds(nx + 1, ny) && !CellVisited(nx + 1, ny)) 
            {
                nodesInfo[nx, ny].node.SetEastWall(false);
                nodesInfo[nx + 1, ny].node.SetWestWall(false);
                nx++;
            }
            else if(newDirection == (int)Directions.South && IsWithinBounds(nx, ny + 1) && !CellVisited(nx, ny + 1)) 
            {
                nodesInfo[nx, ny].node.SetSouthWall(false);
                nodesInfo[nx, ny + 1].node.SetNorthWall(false);
                ny++;
            }
            else if(newDirection == (int)Directions.West && IsWithinBounds(nx - 1, ny) && !CellVisited(nx - 1, ny)) 
            {
                nodesInfo[nx, ny].node.SetWestWall(false);
                nodesInfo[nx - 1, ny].node.SetEastWall(false);
                nx--;
            }

            nodesInfo[nx, ny].visited = true;
        }
    }

    public void Hunt()
    {
        for(int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                if(!CellVisited(x, y) && NextToCellVisited(x, y))
                {
                    OpenPathWithNeighbor(x , y);
                    nx = x;
                    ny = y;
                    nodesInfo[nx, ny].visited = true;
                    return;
                }
            }
        }
        // Debug.Log(allCellsVisited);
        allCellsVisited = true;
    }

    private bool FindWalkablePath(int x, int y)
    {
        // if(!IsWithinBounds(x, y)) return false;
        if(IsWithinBounds(x, y - 1) && !CellVisited(x, y - 1)) return true;
        if(IsWithinBounds(x, y + 1) && !CellVisited(x, y + 1)) return true;
        if(IsWithinBounds(x - 1, y) && !CellVisited(x - 1, y)) return true;
        if(IsWithinBounds(x + 1, y) && !CellVisited(x + 1, y)) return true;
        return false;
    }

    private bool IsWithinBounds(int x, int y)
    {
        return (x >= 0 && x < width && y >= 0 && y < height);
    }

    private bool CellVisited(int x, int y)
    {
        return nodesInfo[x, y].visited;
    }

    private bool NextToCellVisited(int x, int y)
    {
        if(y > 0 && CellVisited(x, y - 1)) return true;
        if(x < width - 2 && CellVisited(x + 1, y)) return true;
        if(y < height - 2 && CellVisited(x, y + 1)) return true;
        if(x > 0 && CellVisited(x - 1, y)) return true;
        return false;
    }

    private void OpenPathWithNeighbor(int x, int y)
    {
        List<int> neighborsDirections = new List<int>();
        
        if(y > 0 && CellVisited(x, y - 1))
        {
            neighborsDirections.Add(1);
        }
        if(x < width - 2 && CellVisited(x + 1, y))
        {
            neighborsDirections.Add(2);
        }
        if(y < height - 2 && CellVisited(x, y + 1))
        {
            neighborsDirections.Add(3);
        }
        if(x > 0 && CellVisited(x - 1, y))
        {
            neighborsDirections.Add(4);
        }

        int newDirection = neighborsDirections[Random.Range(0, neighborsDirections.Count)];

        if(newDirection == (int)Directions.North) 
        {
            nodesInfo[x, y].node.SetNorthWall(false);
            nodesInfo[x, y - 1].node.SetSouthWall(false);
        }
        else if(newDirection == (int)Directions.East) 
        {
            nodesInfo[x, y].node.SetEastWall(false);
            nodesInfo[x + 1, y].node.SetWestWall(false);
        }
        else if(newDirection == (int)Directions.South) 
        {
            nodesInfo[x, y].node.SetSouthWall(false);
            nodesInfo[x, y + 1].node.SetNorthWall(false);
        }
        else if(newDirection == (int)Directions.West) 
        {
            nodesInfo[x, y].node.SetWestWall(false);
            nodesInfo[x - 1, y].node.SetEastWall(false);
        }
    }

}
