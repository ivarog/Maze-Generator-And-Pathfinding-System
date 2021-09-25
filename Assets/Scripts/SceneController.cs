using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] int width;
    [SerializeField] int height;

    Graph graph = new Graph();

    private void Start() 
    {
        graph.Init(width, height);
    }
}
