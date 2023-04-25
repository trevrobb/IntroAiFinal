using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class openDoor : MonoBehaviour
{
    public int currentLvl = 1;
    void Start()
    {
        
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
            if (currentLvl != 4)
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
