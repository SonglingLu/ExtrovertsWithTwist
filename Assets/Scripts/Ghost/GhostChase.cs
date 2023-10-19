using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GhostChase : MonoBehaviour { 

    public Transform player;
    public float chaseSpeed = 3.0f;
    private bool isChasing = false;
    private FollowThePath followThePath;
    private Rigidbody2D rigidbody2D;

    // public Transform visionConeSprite;

    // Start is called before the first frame update
    void Start()
    {
        followThePath = GetComponent<FollowThePath>();
        //rigidbody2D = GetComponent<Rigidbody2D>();
        
        Debug.Log("Mesh starts");
        

        // mesh = new Mesh();
        // GetComponent<MeshFilter>().mesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {
        if(isChasing)
        {
            followThePath.enabled = false;
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
         
            transform.position += directionToPlayer * chaseSpeed * Time.deltaTime;
            //Vector2 newPosition = rigidbody2D.position + directionToPlayer * chaseSpeed * Time.deltaTime;  

            //rigidbody2D.MovePosition(newPosition);
            Vector3 diff = player.position - transform.position;
                    diff.Normalize();          
                    float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;         
                    transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        }
        else
        {
            //rigidbody2D.velocity = Vector2.zero;
            followThePath.enabled = true;
        }


        GhostRaycast();
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isChasing = true;
            Debug.Log("Player entered the ghost's detection radius!");

        
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
    {
        isChasing = false;
        Debug.Log("Player exited the ghost's detection radius!");
    }
        
    }

    public void StopChasing()
    {
        isChasing = false;
    }

// Start is called before the first frame update
    //private Mesh mesh;
    //public LayerMask layerMask;
    float fov = 90f;

    private Vector3 origin;
 
   
    private void GhostRaycast() {

        
        //Debug.Log("Mesh updates");
        
        //Vector3 origin = Vector3.zero;
        Vector3 origin = transform.position;
        int rayCount = 50;
        float angle = 0f;
        float angleIncrease = fov / rayCount;
        float viewDistance = 10f;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];

        vertices[0] = origin;

        int vertexIndex = 1;
        
        for (int i = 0; i <= rayCount; i++) {

            Vector3 vertex;
            //vertex = origin + GetVectorFromAngle(angle) * viewDistance;
             Vector2 rayDirection = Quaternion.Euler(0, 0, angle +fov/2) * transform.up;

            RaycastHit2D raycastHit2D = Physics2D.Raycast( transform.position, rayDirection, viewDistance);
            if (raycastHit2D.collider == null) {
                // no hit
                vertex = origin + GetVectorFromAngle(angle) * viewDistance;
                Debug.Log("No hit");
            }
            else {
                vertex = raycastHit2D.point;
                Debug.Log("Hit : "+ raycastHit2D.transform.gameObject.name);

            if (raycastHit2D.transform.CompareTag("Player"))
        {
            isChasing = true;
            Debug.Log("Player entered the ghost's detection radius!");
        
        }


            } 
            //Debug.Log("z rotation value:" + transform.rotation.z);
           
            Debug.DrawRay(transform.position,rayDirection * viewDistance, Color.red);

            vertices[vertexIndex] = vertex;

            vertexIndex++;
            angle -= angleIncrease;
        }
         

        
        
    }

    public void SetOrigin(Vector3 origin) {
        this.origin = origin;

    }

    private static Vector3 GetVectorFromAngle(float angle) {
        float angleRad = (float)(angle * (Mathf.PI/180f));
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    // Triangle[] TriangulatePoints(Vector3[] points)
    // {
    //     Triangle[] triangles = new Triangle[points.Length - 2];

    //     if (points.Length < 3)
    //         return triangles; // Not enough points to form triangles

    //     // Assuming points are in a counter-clockwise order (important for correct rendering)
    //     Vector2 firstPoint = points[0];
    //     for (int i = 1; i < points.Length - 1; i++)
    //     {
    //         triangles[i - 1] = new Triangle(firstPoint, points[i], points[i + 1]);
    //     }

    //     return triangles;
    // }

}


    

// public class Triangle
// {
//     public Vector3 Point1 { get; }
//     public Vector3 Point2 { get; }
//     public Vector3 Point3 { get; }

//     public Triangle(Vector3 point1, Vector3 point2, Vector3 point3)
//     {
//         Point1 = point1;
//         Point2 = point2;
//         Point3 = point3;
//     }
//}