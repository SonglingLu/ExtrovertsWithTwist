using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    private Rigidbody2D ghostRB;
    private float moveSpeed = 4f;
    private int waypointIndex = 0;
    private Vector3 lastPosition;
    private bool chase;

    [SerializeField] private Transform pfFieldOfView;
    private FieldOfView fieldOfView;

    public GameObject player;

    private void Start()
    {
        if (waypoints.Length > 0)
        {
            transform.position = waypoints[waypointIndex].position;
        }

        ghostRB = transform.GetComponent<Rigidbody2D>();

        fieldOfView = Instantiate(pfFieldOfView, null).GetComponent<FieldOfView>();
        fieldOfView.SetGhostMovement(this);
        fieldOfView.SetOrigin(transform.position);
    }

    private void Update()
    {
        if (chase) {
            Chase();
        } else {
            Move();
        }

        fieldOfView.SetOrigin(transform.position);
        if (transform.position != lastPosition) {
            Vector3 moveDir = (transform.position - lastPosition).normalized;
            fieldOfView.SetDirection(moveDir);
        }
 
        lastPosition = transform.position;
        
    }


    private void Move()
    {
        if (waypointIndex < waypoints.Length)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].position, step);

            /* Vector3 diff = transform.position - lastPosition;
                    diff.Normalize();          
                    float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;         
                    transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90); */


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

    private void Chase()
    {
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
        transform.position += directionToPlayer * moveSpeed * Time.deltaTime;
    }

    public void SetChase(bool chase) {
        this.chase = chase;
    }
    
}