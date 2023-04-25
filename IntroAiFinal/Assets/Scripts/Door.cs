using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] GameObject openDoor;
    public bool shouldOpen;
    public static Door Instance;
    public bool alreadyOpen;
    void Start()
    {
        shouldOpen = false;
        Instance = this;
        alreadyOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldOpen && !alreadyOpen)
        {
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            Instantiate(openDoor, transform.position, Quaternion.identity);
            alreadyOpen = true;
        }  
    }
}
