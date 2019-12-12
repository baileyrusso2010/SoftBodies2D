using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBody : MonoBehaviour
{
    public float mass;

    public Vector2 position;
    public Vector2 velocity;
    public Vector2 acceleration;
    public Vector2 gravity;

    public bool isGravity;

    // Start is called before the first frame update
    void Start()
    {
        if(isGravity)
            gravity = new Vector2(0, -9.81f);
        velocity = acceleration = Vector2.zero;
    }

    void Update()
    {
        Move();   
    }

    public void ApplyForce(Vector3 force)
    {
        acceleration += (Vector2)force / mass;

    }//end of Apply force

    void Move()
    {
 
        acceleration = Vector2.zero;

        velocity += this.acceleration;
        position += this.velocity* Time.deltaTime;

        transform.position = new Vector3(position.x, position.y,0);
        this.acceleration = Vector2.zero;

    }//end of move

}//end of script