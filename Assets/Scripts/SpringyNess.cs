using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpringyNess : MonoBehaviour
{

    public GameObject bottomLeft, topLeft, topRight, BottomRight;

    float spring = .2f;
    float friction = 0.55f;
    float springLength = 5;

    Mesh mesh;
    public Material mat;

    Vector3[] verticies = new Vector3[4];

    void UpdateVerticies()
    {
        //bottom left, top left, top right, bottom right
        verticies[0] = new Vector3(bottomLeft.transform.position.x, bottomLeft.transform.position.y);
        verticies[1] = new Vector3(topLeft.transform.position.x, topLeft.transform.position.y);
        verticies[2] = new Vector3(topRight.transform.position.x, topRight.transform.position.y);
        verticies[3] = new Vector3(BottomRight.transform.position.x, BottomRight.transform.position.y);

    }//end of UpdateVerticies

    void Start()
    {
        mesh = new Mesh();//instantiates new mesh

        UpdateVerticies();//updates the posiitions

        mesh.vertices = verticies;//updates verticies
        mesh.triangles = new int[] { 0, 1, 2, 0, 2, 3 };//creates box
        List<Color> cr = new List<Color>();//color

        //sets color and mesh
        GetComponent<MeshRenderer>().material = mat;
        GetComponent<MeshFilter>().mesh = mesh;

        topLeft.GetComponent<RigidBody>().position = topLeft.transform.position;
        bottomLeft.GetComponent<RigidBody>().position = bottomLeft.transform.position;
        BottomRight.GetComponent<RigidBody>().position = BottomRight.transform.position;
        topRight.GetComponent<RigidBody>().position = this.transform.position;

    }

    void Update()
    {

        UpdateVerticies();

        mesh.vertices = verticies;

        for (uint i = 0; i < verticies.Length; i++)
        {
            if (LineCollision(0, 0, 0, 3, verticies[i].x, verticies[i].y))
            {
                Debug.Log("is colliding");
                if (i == 0)
                    bottomLeft.GetComponent<RigidBody>().ApplyForce(Vector3.left);
                if (i == 1)
                    topLeft.GetComponent<RigidBody>().ApplyForce(Vector3.left);
                if (i == 2)
                    topRight.GetComponent<RigidBody>().ApplyForce(Vector3.left);
                if (i == 3)
                    BottomRight.GetComponent<RigidBody>().ApplyForce(Vector3.left);

            }
        }

        //bottom left spring
        SpringTo(bottomLeft, topLeft);
        SpringTo(bottomLeft, BottomRight);
        SpringTo(bottomLeft, topRight);

        //top left spring
        SpringTo(topLeft, bottomLeft);
        SpringTo(topLeft, topRight);
        SpringTo(topLeft, BottomRight);

        //top right spring
        SpringTo(topRight, topLeft);
        SpringTo(topRight, BottomRight);
        SpringTo(topRight, bottomLeft);

        //bottom right spring
        SpringTo(BottomRight, bottomLeft);
        SpringTo(BottomRight, topRight);
        SpringTo(BottomRight, topLeft);
            
            
    }//end of update
    

    /// <summary>
    /// Controls the springs
    /// </summary>
    /// <param name="one"></param>
    /// <param name="two"></param>
    void SpringTo(GameObject one, GameObject two)
    {
        //gets distance btw two object
        float dx = two.transform.position.x - one.transform.position.x;
        float dy = two.transform.position.y - one.transform.position.y;

        //obtains angle
        float angle = Mathf.Atan2(dy, dx);

        //target position
        float targetX = two.transform.position.x - Mathf.Cos(angle) * springLength;
        float targetY = two.transform.position.y - Mathf.Sin(angle) * springLength;

        //
        float vx = (targetX - one.transform.position.x) * spring;
        float vy = (targetY - one.transform.position.y) * spring;

        //slows it down
        vx *= friction;
        vy *= friction;

        //updates the position
        one.transform.position += new Vector3(vx, vy, 0);

    }//end of springTo

    /// <summary>
    /// For each point in this
    /// check against the walls
    /// to see if they collide
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="y1"></param>
    /// <param name="x2"></param>
    /// <param name="y2"></param>
    /// <param name="px"></param>
    /// <param name="py"></param>
    /// <returns></returns>
    bool LineCollision(float x1, float y1, float x2, float y2, float px, float py)
    {
        //gets distnace from point to first point on line
        float d1 = Vector2.Distance(new Vector2(px, py), new Vector2(x1, y1));

        //gets distance from point to last point on line
        float d2 = Vector2.Distance(new Vector2(px, py), new Vector2(x2, y2));

        //gets the line distance
        float lineLen = Vector2.Distance(new Vector2(x1, y1), new Vector2(x2, y2));

        //how much space it has to work with
        float buffer = 0.1f;

        //if is on the line
        if (d1 + d2 >= lineLen - buffer && d1 + d2 <= lineLen + buffer)
            return true;

        return false;
    }//end of Check Collision

}//end of scripts