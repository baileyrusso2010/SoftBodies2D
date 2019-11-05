using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{

    public Vector2 orgin;

    public Vector2 position, velocity, accelleration;

    public float restLength;//original length
    public float mass;

    const float k = .8f;


    void Start()
    {
        mass = 4;
        restLength = -3.11f;
        orgin = Vector2.zero;
        velocity = accelleration = Vector2.zero;
    }

    void Update()
    {

        Vector2 spring = (Vector2) this.transform.position - orgin;//length of spring
        float currentLength = spring.magnitude;//scalar

        spring = spring.normalized;
        float stretch = currentLength - restLength;//displacement
        spring *= (-k * stretch);
        ApplyForce(spring);

        Vector2 gravity = new Vector2(0, -9.0f);//applys force downward
        ApplyForce(gravity);

        Move();
        Debug.DrawLine(orgin, this.transform.position);

    }

    void ApplyForce(Vector2 force)
    {
        accelleration += force / mass;
    }//end of applyForce

    void Move()
    {
        velocity += accelleration;
        position += velocity * Time.deltaTime;
        this.transform.position = position;
        accelleration = Vector2.zero;
    }

}//end of class