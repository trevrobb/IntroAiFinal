
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class Monster : MonoBehaviour
{
    private int currentPathIndex;
    private List<Vector3> pathVectorList;
    [SerializeField] float speed;
    Vector3 playerPos;
    private Pathfinding pathfinding;
    [SerializeField] Player player;
    Rigidbody2D rb;



    int currentWaypoint = 0;
    bool reachedEndOfPath = false;


    [SerializeField] Graph myGraph;
    [SerializeField] Node start;
    [SerializeField] Node end;
    [SerializeField] Tilemap wallTilemap;

    void Start()
    {

        if (SceneManager.GetActiveScene().buildIndex == 1) start.transform.position = this.transform.position;



        pathfinding = new Pathfinding(45, 20);


        rb = GetComponent<Rigidbody2D>();

       
        foreach (var pos in wallTilemap.cellBounds.allPositionsWithin)
        {

            if (wallTilemap.HasTile(pos))
            {
                
                if (pathfinding.GetGrid().GetGridObject(wallTilemap.CellToWorld(pos)) != null)
                {
                    GridCell c = pathfinding.GetGrid().GetGridObject(wallTilemap.CellToWorld(pos));
                    c.isWalkable = false;
                    Debug.DrawLine(new Vector3(wallTilemap.CellToWorld(pos).x, wallTilemap.CellToWorld(pos).y), new Vector3(wallTilemap.CellToWorld(pos).x + 1, wallTilemap.CellToWorld(pos).y), Color.red, 100f);
                    Debug.DrawLine(new Vector3(wallTilemap.CellToWorld(pos).x, wallTilemap.CellToWorld(pos).y), new Vector3(wallTilemap.CellToWorld(pos).x, wallTilemap.CellToWorld(pos).y+1), Color.red, 100f);

                }
                
            }

        }


    }

    // Update is called once per frame
    void Update()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;



        pathfinding = new Pathfinding(45, 20);





    }
    private void FixedUpdate()
    {

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            //basicMovement();

            SetTargetPosition(playerPos);
            if (pathVectorList != null)
            {
                Movement();
                
            }

        }
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            start.transform.position = this.transform.position;
            end.transform.position = playerPos;
            Dij();
        }


    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        currentPathIndex = 0;
        pathVectorList = pathfinding.FindPath(transform.position, targetPosition);
        
        

        if (pathVectorList != null && pathVectorList.Count > 1)
        {
            pathVectorList.RemoveAt(0);
            
        }

    }
    private void Movement()
    {
        if (pathVectorList != null)
        {

            Vector3 targetPosition = pathVectorList[currentPathIndex];

            if (Vector3.Distance(targetPosition, transform.position) > .001f)
            {
                Vector3 moveDir = (targetPosition - transform.position);



                transform.position = transform.position + moveDir * speed * Time.deltaTime;

            }
            else
            {
                currentPathIndex++;
                if (currentPathIndex >= pathVectorList.Count)
                {
                    StopMoving();
                }
            }


        }
        
   }
        
    

    private void StopMoving()
    {
        pathVectorList = null;
    }

    private void basicMovement()
    {
        rb.AddForce((playerPos - transform.position) * .5f);
       
    }

 

    void Dij()
    {
        Path path = myGraph.GetShortestPath(start, end);
        for (int i = 0; i < path.nodes.Count; i++)
        {
            if (Vector3.Distance(transform.position, path.nodes[i].transform.position) > .01f)
            {
                Vector3 moveDir = (path.nodes[i].transform.position - transform.position).normalized;



                transform.position = transform.position + moveDir * speed * Time.deltaTime;

            }
        }
    }
}
