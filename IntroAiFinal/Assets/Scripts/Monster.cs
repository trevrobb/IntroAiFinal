using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Monster : MonoBehaviour
{
    private int currentPathIndex;
    private List<Vector3> pathVectorList;
    [SerializeField] int speed;
    Vector3 playerPos;
    private Pathfinding pathfinding;
    [SerializeField] Player player;

    public Pathfinding Pathfinding { get => pathfinding; set => pathfinding = value; }

    void Start()
    {
        
        
        //pathfinding = new Pathfinding(10, 10);
        

       

        playerPos = new Vector3(player.transform.position.x, player.transform.position.y);



    }

    // Update is called once per frame
    void Update()
    {
        
        if (pathVectorList != null)
        {
            //Movement();
            

        }
        

      
        
        
        


    }
    private void FixedUpdate()
    {
        //SetTargetPosition(player.transform.position);


    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        currentPathIndex = 0;
        pathVectorList = Pathfinding.FindPath(transform.position, targetPosition);
        
        

        if (pathVectorList != null && pathVectorList.Count > 1)
        {
            pathVectorList.RemoveAt(0);
            
        }

    }
    private void Movement()
    {
        if (pathVectorList != null)
        {
            Debug.Log(pathVectorList[currentPathIndex]);
            Vector3 targetPosition = pathVectorList[currentPathIndex];
            
            if (Vector3.Distance(transform.position, targetPosition) > .01f)
            {
                Vector3 moveDir = (targetPosition - transform.position).normalized;
                Debug.Log(transform.position);
                Debug.Log(moveDir);
                
                transform.position = transform.position + moveDir * speed * Time.deltaTime;

            }
            else
            {
                currentPathIndex++;
                if (currentPathIndex >= pathVectorList.Count)
                {
                    pathVectorList = null;
                }
            }
        }
        
    }

    private void StopMoving()
    {
        pathVectorList = null;
    }
}
