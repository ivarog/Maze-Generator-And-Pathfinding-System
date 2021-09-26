using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    private int x;
    private int y;
    Vector3 position;
    [SerializeField] float speed = 0.2f;
    private bool walking;
    public bool IsWalking
    {
        get{return walking;}
    }

    public void Init(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public void Walk(List<Node> path)
    {
        walking = true;
        StartCoroutine(WalkingRoutine(path));
    }

    IEnumerator WalkingRoutine(List<Node> path)
    {
        int i = 0;

        while(i < path.Count)
        {
            transform.position = path[i].position;
            i++;
            yield return new WaitForSeconds(speed);
        }

        walking = false;
        yield break;
    }
}
