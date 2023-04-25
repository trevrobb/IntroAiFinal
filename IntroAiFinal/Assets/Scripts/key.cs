using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class key : MonoBehaviour
{
    public int currentLvl = 1;
    public static key Instance;
    void Start()
    {
        Instance = this;
        if (GetComponent<Renderer>() != null)
        {
            GetComponent<Renderer>().enabled = false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Door.Instance.shouldOpen = true;
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
