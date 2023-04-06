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
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(player.transform.position);
        pathVectorList = new List<Vector3>();
        SetTargetPosition(player.transform.position);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
       
        if (player != null)
        {
            
            //Movement();
            

        }
        
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        currentPathIndex = 0;
        pathVectorList = Pathfinding.Instance.FindPath(transform.position, targetPosition);

        Debug.Log(pathVectorList.Count);

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
            
            if (Vector3.Distance(transform.position, targetPosition) > 1f)
            {
                Vector3 moveDir = (targetPosition - transform.position).normalized;

                float distanceBefore = Vector3.Distance(transform.position, targetPosition);
                transform.position = targetPosition + moveDir * speed *Time.deltaTime;

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
}
