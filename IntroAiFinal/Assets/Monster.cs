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
    Player player;
    private Pathfinding pathfinding;
    void Start()
    {
        player = Player.instance.GetComponent<Player>();
        
        pathfinding = new Pathfinding(25, 15);
        pathVectorList = new List<Vector3>();


        
        



    }

    // Update is called once per frame
    void Update()
    {
        player = Player.instance.GetComponent<Player>();
        if (pathVectorList != null)
        {
            //Movement();
        }
        

        SetTargetPosition(player.transform.position);
        




    }
    private void FixedUpdate()
    {
       
        
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        currentPathIndex = 0;
        pathVectorList = Pathfinding.Instance.FindPath(transform.position, targetPosition);
        Debug.Log(pathVectorList.Count);
        

        if (pathVectorList != null && pathVectorList.Count > 1)
        {
            pathVectorList.RemoveAt(0);
            Debug.Log(pathVectorList[0]);
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
        else
        {
            pathVectorList = null;
        }    
    }

    private void StopMoving()
    {
        pathVectorList = null;
    }
}
