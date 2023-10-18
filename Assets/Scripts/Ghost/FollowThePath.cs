using UnityEngine;

public class FollowThePath : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    private Rigidbody2D ghostRB;
    private float moveSpeed = 4f;
    private int waypointIndex = 0;

    private void Start()
    {
        if (waypoints.Length > 0)
        {
            transform.position = waypoints[waypointIndex].position;
        }

        ghostRB = transform.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
        lastPosition = transform.position;
       
    }
    Vector3 lastPosition;
    private void Move()
    {
        if (waypointIndex < waypoints.Length)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].position, step);

            Vector3 diff = transform.position - lastPosition;
                    diff.Normalize();          
                    float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;         
                    transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);


            if (Vector2.Distance(transform.position, waypoints[waypointIndex].position) < 0.01f)
            {
                waypointIndex++;
                if (waypointIndex >= waypoints.Length)
                {
                    waypointIndex = 0; // Loop back to the first waypoint
                }
            }
        }
    }
    
}