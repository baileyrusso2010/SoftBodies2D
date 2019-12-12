using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public RigidBody first, ground;


    void Update()
    {
        if (IsColliding() == true)
        {
            first.gravity = Vector2.zero;
            ElasticCollision(first, ground);

            //Debug.Log(ground.transform.position.y + ground.transform.localPosition.y);
        }
        else
            first.gravity = new Vector2(0, -9.81f);

    }//end of update

    void ElasticCollision(RigidBody one, RigidBody two)
    {
        if (one == two)
            return;

        float mass1 = one.mass;
        float mass2 = two.mass;

        Vector3 aF = ((mass1 - mass2) / (mass1 + mass2) * one.velocity) + (2 * mass2) /
            (mass1 + mass2) * two.velocity;

        //Vector3 bF = ((2 * mass1) / (mass1 + mass2) * one.velocity) + (mass2 - mass1) /
                                                            //        (mass1 + mass2) * two.velocity;

        one.velocity = aF;
        //two.velocity = bF;

    }

    bool IsColliding()
    {
        return (first.transform.position.x < ground.transform.position.x + ground.transform.localScale.x &&
            first.transform.position.x + first.transform.localScale.x > ground.transform.position.x &&
            first.transform.position.y < ground.transform.position.y + ground.transform.localScale.y &&
            first.transform.position.y + first.transform.localScale.y > ground.transform.position.y);
    }//end of isColliding
}//endo f script
