using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    /*
     * This is what I originally
     * had just for two springs
     * but decided to have an
     * a bunch of connected
     * springs, but this is
     * a good reference to
     * have if I wanted to use
     * springs later in my career
     */



    public GameObject one, two;

    float positionY;
    float velocityY;
    //Vector2 acceleration;
    float gravity;
    float anchorY;
    float anchorX;

    const float K = 7f;
    float mass = 7;
    float damping = 2;
    float velocityX = 0;
    float positionX;

    float mass1VelocityX = 0;
    float mass1VelocityY = 0;
    float mass1PositionY = 5;
    float mass1PositionX = 5;

    float mass2PositionY = 7;
    float mass2PositionX = 7;
    float mass2VelocityX = 0;
    float mass2VelocityY = 0;


    void Start()
    {
        
        gravity = -3;
        anchorY = 0;
        positionY = 10;
    }

    void Update()
    {

        // Mass 1 Spring Force
        var mass1SpringForceY = -K * (mass1PositionY - anchorY);
        var mass1SpringForceX = -K * (mass1PositionX - anchorX);

        // Mass 2 Spring Force
        var mass2SpringForceY = -K * (mass2PositionY - mass1PositionY);
        var mass2SpringForceX = -K * (mass2PositionX - mass1PositionX);

        // Mass 1 daming
        var mass1DampingForceY = damping * mass1VelocityY;
        var mass1DampingForceX = damping * mass1VelocityX;

        // Mass 2 daming
        var mass2DampingForceY = damping * mass2VelocityY;
        var mass2DampingForceX = damping * mass2VelocityX;

        // Mass 1 net force
        var mass1ForceY = mass1SpringForceY + mass * gravity - mass1DampingForceY - mass2SpringForceY + mass2DampingForceY;

        var mass1ForceX = mass1SpringForceX - mass1DampingForceX - mass2SpringForceX + mass2DampingForceX;

        // Mass 2 net force
        var mass2ForceY = mass2SpringForceY + mass * gravity - mass2DampingForceY;
        var mass2ForceX = mass2SpringForceX - mass2DampingForceX;

        // Mass 1 acceleration
        var mass1AccelerationY = mass1ForceY / mass;
        var mass1AccelerationX = mass1ForceX / mass;

        // Mass 2 acceleration
        var mass2AccelerationY = mass2ForceY / mass;
        var mass2AccelerationX = mass2ForceX / mass;

        // Mass 1 velocity
        mass1VelocityY = mass1VelocityY + mass1AccelerationY * Time.deltaTime;
        mass1VelocityX = mass1VelocityX + mass1AccelerationX * Time.deltaTime;

        // Mass 2 velocity
        mass2VelocityY = mass2VelocityY + mass2AccelerationY * Time.deltaTime;
        mass2VelocityX = mass2VelocityX + mass2AccelerationX * Time.deltaTime;

        // Mass 1 position
        mass1PositionY = mass1PositionY + mass1VelocityY * Time.deltaTime;
        mass1PositionX = mass1PositionX + mass1VelocityX * Time.deltaTime;

        // Mass 2 position
        mass2PositionY = mass2PositionY + mass2VelocityY * Time.deltaTime;
        mass2PositionX = mass2PositionX + mass2VelocityX * Time.deltaTime;

        one.transform.position = new Vector3(mass1PositionX, mass1PositionY, 0);
        two.transform.position = new Vector3(mass2PositionX, mass2PositionY, 0);

        //var forceY = springForceY + mass * gravity;
        //var acceleration = forceY / mass;
        //velocityY = velocityY + acceleration * Time.deltaTime;
        //this.positionY = positionY + velocityY * Time.deltaTime;


        Debug.DrawLine(new Vector3(anchorX,anchorY), this.transform.position);

    }


}//end of class