using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool walkable = true;
    public bool current = false;
    public bool target = false;
    public bool selectable = false;
    //Needed BFS(Breadth Fisrt Search)
    public bool visited = false;
    public Tile parent = null;
    public int distance = 0;

    public List<Tile> adjacencyLists = new List<Tile>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (current)
        {
            GetComponent<Renderer>().material.color = Color.magenta;
        }
        else if (target)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        else if (selectable)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
    }

    public void Reset()
    {
        adjacencyLists.Clear();
        current = false;
        target = false;
        selectable = false;
        visited = false;
        parent = null;
        distance = 0;
    }

    public void FindNeighbors(float jumpHeigth)
    {
        Reset();
        Checktile(Vector3.forward, jumpHeigth);
        Checktile(-Vector3.forward, jumpHeigth);
        Checktile(Vector3.right, jumpHeigth);
        Checktile(-Vector3.right, jumpHeigth);
    }

    public void Checktile(Vector3 direction, float jumpHeigth)
    {
        Vector3 halfExtends = new Vector3(0.25f, (1 + jumpHeigth) / 2.0f, 0.25f);
        Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtends);
        foreach(Collider item in colliders)
        {
            Tile tile = item.GetComponent<Tile>();
            if (tile != null && tile.walkable)
            {
                RaycastHit hit;
                if (Physics.Raycast(tile.transform.position, Vector3.up, out hit, 1))
                {
                    adjacencyLists.Add(tile);
                }
            }
        }
    }
}
