// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class GhostChase : MonoBehaviour { 

//     public Transform player;
//     private float chaseSpeed = 4f;
//     private bool isChasing = false;
//     private GhostMovement ghostMovement;
//     private Rigidbody2D rigidbody2D;

//     public Transform visionConeSprite;



//     // Start is called before the first frame update
//     void Start()
//     {
//         ghostMovement = GetComponent<GhostMovement>();
//         //rigidbody2D = GetComponent<Rigidbody2D>();
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if(isChasing)
//         {
//             Debug.Log("Ghost starts chasing!!");
//             ghostMovement.enabled = false;

//             Vector3 directionToPlayer = (player.position - transform.position).normalized;
         
//             transform.position += directionToPlayer * chaseSpeed * Time.deltaTime;
//             //Vector2 newPosition = rigidbody2D.position + directionToPlayer * chaseSpeed * Time.deltaTime;  

//             //rigidbody2D.MovePosition(newPosition);
//             Vector3 diff = player.position - transform.position;
//                     diff.Normalize();          
//                     float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;         
//                     transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
//         }
//         else
//         {
//             //rigidbody2D.velocity = Vector2.zero;
//             ghostMovement.enabled = true;
//         }
        
//     }

//     void OnTriggerEnter2D(Collider2D other)
//     {
//         InvisibleMechanic invisibleMechanic = other.GetComponent<InvisibleMechanic>();
//         if (other.CompareTag("Player")  && (invisibleMechanic == null || !invisibleMechanic.IsPlayerCloaked() ) )
//         {
//             isChasing = true;
//             Debug.Log("Player entered the ghost's detection radius!");

        
//         }
//     }

//     void OnTriggerExit2D(Collider2D other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             isChasing = false;
//             Debug.Log("Player exited the ghost's detection radius!");
//         }
        
//     }

//     public void StopChasing()
//     {
//         isChasing = false;
//     }


// }
