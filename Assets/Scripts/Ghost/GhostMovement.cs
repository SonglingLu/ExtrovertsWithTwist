using System.Collections;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    private Rigidbody2D ghostRB;
    public float moveSpeed = 4f;
    private int waypointIndex = 0;
    private Vector3 lastPosition;

    private bool chase;
    private bool blindChase;
    private bool wasChasing =  false;
    private float chaseSpeed = 5f;

    private GameObject distraction;
    private bool distractionExist = false;
    private bool distracted = false;
    private float distractionRange = 4f;

   
    private InvisibleMechanic invisibleMechanic;


    [SerializeField] private Transform pfFieldOfView;
    private FieldOfView fieldOfView;

    public GameObject player;

    public GameObject InvisibleButton;
    private InvisibleBrush invisibleBrush;

    private void Start()
    {
        invisibleBrush = InvisibleButton.GetComponent<InvisibleBrush>();

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
        bool IsPlayerCloaked = invisibleBrush.isCloaked;

        if (distractionExist) {
            distracted = Vector2.Distance(distraction.transform.position, transform.position) <= distractionRange;

        } else {
            distracted = false;
        }

        Vector3 moveDir;

        if (distracted) {
            if (Vector2.Distance(distraction.transform.position, transform.position) < 2f) {
                moveDir = (distraction.transform.position - transform.position).normalized;
            } else {
                DistractChase();
                moveDir = (transform.position - lastPosition).normalized;
            }
        } else {
            if (IsPlayerCloaked) {
                chase = false;
                blindChase = false;
                Move();
            } else {
                if (chase || blindChase) {
                    Chase();
                    wasChasing = true;
                } else {
                    if(wasChasing && !FindAnyObjectByType<FirebaseManager>().playerKilled) {
                        wasChasing = false;
                   
                        StartCoroutine( FindAnyObjectByType<FirebaseManager>().updateGhostAnalytics(true));
                    }
                    Move();
                }
            }

            moveDir = (transform.position - lastPosition).normalized;
        }

        fieldOfView.SetOrigin(transform.position);
        if ((distracted && Vector2.Distance(distraction.transform.position, transform.position) < 0.02f) || transform.position != lastPosition) {
            fieldOfView.SetDirection(moveDir);
        }

        lastPosition = transform.position;
        
    }

    float angle;
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

            angle = Vector2.SignedAngle(transform.right, movementDirection);
            transform.Rotate(Vector3.forward, 20f * angle * Time.deltaTime);

        }
    }

    private void Chase()
    {

        //if(invisibleMechanic.isCloaked) return;

        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
        
        //transform.position += directionToPlayer * moveSpeed * Time.deltaTime;
        ghostRB.velocity = directionToPlayer * chaseSpeed;
       
        angle = Vector2.SignedAngle(transform.right, directionToPlayer);
        transform.Rotate(Vector3.forward, 20f * angle * Time.deltaTime);
    }

    private void DistractChase()
    {
        Vector3 directionToDistraction = (distraction.transform.position - transform.position).normalized;
        
        ghostRB.velocity = directionToDistraction * moveSpeed;
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
    
    public void setNewDistraction(GameObject distraction) {
        this.distraction = distraction;
    }

    public void setDistractionExist(bool distractionExist) {
        this.distractionExist = distractionExist;
    }
}