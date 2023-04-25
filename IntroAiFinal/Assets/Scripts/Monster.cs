
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using Pathfinding;

public class Monster : MonoBehaviour
{
    private int currentPathIndex;
    private List<Vector3> pathVectorList;
    [SerializeField] float speed;
    Vector3 playerPos;
    private Pathfinding2 pathfinding;
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

        if (SceneManager.GetActiveScene().buildIndex == 2) start.transform.position = this.transform.position;



        pathfinding = new Pathfinding2(45, 20);


        rb = GetComponent<Rigidbody2D>();

        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            foreach (var pos in wallTilemap.cellBounds.allPositionsWithin)
            {

                if (wallTilemap.HasTile(pos))
                {

                    if (pathfinding.GetGrid().GetGridObject(wallTilemap.CellToWorld(pos)) != null)
                    {
                        GridCell c = pathfinding.GetGrid().GetGridObject(wallTilemap.CellToWorld(pos));
                        c.isWalkable = false;
                        Debug.DrawLine(new Vector3(wallTilemap.CellToWorld(pos).x, wallTilemap.CellToWorld(pos).y), new Vector3(wallTilemap.CellToWorld(pos).x + 1, wallTilemap.CellToWorld(pos).y), Color.red, 100f);
                        Debug.DrawLine(new Vector3(wallTilemap.CellToWorld(pos).x, wallTilemap.CellToWorld(pos).y), new Vector3(wallTilemap.CellToWorld(pos).x, wallTilemap.CellToWorld(pos).y + 1), Color.red, 100f);

                    }

                }

            }
        }
       


    }

    // Update is called once per frame
    void Update()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;



        





    }
    private void FixedUpdate()
    {

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            basicMovement();

            
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            start.transform.position = this.transform.position;
            end.transform.position = playerPos;
            Dij();
        }

        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            /*
            SetTargetPosition(playerPos);

            if (pathVectorList != null)
            {
                Movement();

            }
            */
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

            if (Vector3.Distance(targetPosition, transform.position) > .01f)
            {
                Vector3 moveDir = (targetPosition - transform.position).normalized;



                transform.position = transform.position + moveDir * speed * Time.deltaTime;

            }
            else
            {
                currentPathIndex++;
                
            }


        }
        
   }
        
    

    private void StopMoving()
    {
        pathVectorList = null;
    }

    private void basicMovement()
    {
        rb.AddForce((playerPos - transform.position) * .01f, ForceMode2D.Impulse);
       
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
