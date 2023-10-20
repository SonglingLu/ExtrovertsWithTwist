using System.Collections;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    private Rigidbody2D ghostRB;
    private float moveSpeed = 4f;
    private int waypointIndex = 0;
    private Vector3 lastPosition;
    private bool chase;
    private bool blindChase;

    public bool wasChasing =  false;

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

        chase = false;
        blindChase = false;
    }
    
    private void Update()
    {
        if (chase || blindChase) {
            Chase();
            wasChasing = true;
        } else {
            if(wasChasing && FindAnyObjectByType<FirebaseManager>().currentGhostEscapes==0)
            {
                wasChasing = false;
                FindAnyObjectByType<FirebaseManager>().currentGhostEscapes++;
                StartCoroutine( FindAnyObjectByType<FirebaseManager>().postLevelAnalytics(false, true));

                Debug.Log("Escape " + FindAnyObjectByType<FirebaseManager>().currentGhostEscapes);
            }
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
            Vector3 movementDirection = (waypoints[waypointIndex].position - transform.position).normalized;
            ghostRB.velocity = movementDirection * moveSpeed ;

            //float step = moveSpeed * Time.deltaTime;
            //transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].position, step);

            if (Vector2.Distance(transform.position, waypoints[waypointIndex].position) < 0.05f)
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
        
        //transform.position += directionToPlayer * moveSpeed * Time.deltaTime;
        ghostRB.velocity = directionToPlayer * moveSpeed;
    }

    public void SetChase(bool chase) {
        if (this.chase && !chase) {
            StartCoroutine(ChaseWhileBlind());
        }

        this.chase = chase;
    }

    IEnumerator ChaseWhileBlind() {
        blindChase = true;
        yield return new WaitForSeconds(3f);
        blindChase = false;
    }
    
}