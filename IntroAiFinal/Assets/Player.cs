using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D body;
    float horizontal;
    float vertical;
    Grid grid;
    public float runSpeed = 20.0f;
    
    public static Player instance;
    void Start()
    {
        instance = this;
        body = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        
    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }

    public Vector3 getPlayerPosition()
    {
        return new Vector3(transform.position.x, transform.position.y);
    }
}
