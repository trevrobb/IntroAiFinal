using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rigidbody2D body;
    float horizontal;
    float vertical;
    Grid grid;
    public float runSpeed = 20.0f;
    private float time;
    
    public static Player instance;
    void Start()
    {
        instance = this;
        body = GetComponent<Rigidbody2D>();
        time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        time += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }

    public Vector3 getPlayerPosition()
    {
        return new Vector3(transform.position.x, transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("keyVis"))
        {
            GameObject key = GameObject.FindGameObjectWithTag("key");
            key.GetComponent<Renderer>().enabled = true;
        }
        if (collision.gameObject.CompareTag("MonsterCollide"))
        {
            Debug.Log("Monster found player in " + time + " seconds.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
