using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterCollision : MonoBehaviour
{
    GameObject Monster;
    void Start()
    {
        Monster = GameObject.FindGameObjectWithTag("Monster");
    }

    // Update is called once per frame
    void Update()
    {
        Monster = GameObject.FindGameObjectWithTag("Monster");
        if (Monster)
        {
            this.transform.position = Monster.transform.position;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
