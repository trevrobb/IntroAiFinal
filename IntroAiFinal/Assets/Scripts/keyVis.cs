using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyVis : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject key = GameObject.FindGameObjectWithTag("key");
        if (key)
        {
            this.transform.position = key.transform.position;
        }
    }
}
