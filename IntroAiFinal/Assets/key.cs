using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class key : MonoBehaviour
{
    public int currentLvl = 0;
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
            currentLvl = SceneManager.GetActiveScene().buildIndex;
            currentLvl++;
            if (currentLvl != 3)
            {
                SceneManager.LoadScene(currentLvl);
            }
            else
            {
                SceneManager.LoadScene("SampleScene");
            }
            
        }
    }
}
