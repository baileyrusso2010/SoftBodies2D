using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    public RigidBody player;

    void Start()
    {
        //player.gravity = new Vector2(0, -9.81f);
        player.velocity = player.acceleration = Vector2.zero;
    }

    void Update()
    {
        Move();

    }
    
    void ApplyForce(Vector2 force)
    {
        player.acceleration += force / player.mass;
    }//end of ApplyForce

    void Move()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyForce(Vector2.left * 10);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyForce(Vector2.right * 10);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            ApplyForce(Vector2.up * 50);
        }
        else
        {
            player.velocity *= .95f;
        }

        ApplyForce(player.gravity);


        player.velocity += player.acceleration;
        player.position += player.velocity * Time.deltaTime;
        
        player.transform.position = player.position;
        player.acceleration = Vector2.zero;


    }//end of move

}//end of script