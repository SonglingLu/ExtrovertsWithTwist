using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DistractionController4 : MonoBehaviour
{
    public GameObject distractionPrefab;
    private bool distractionExist = false;
    private float distractionLifetime = 5.0f;

    private Toggle DrawDistractionToggle;

    [SerializeField] GameObject[] ghosts;

    Vector2[] roomMins = new Vector2[]
    {
        new Vector2(-40.95f,-2.53f),
        new Vector2(-36.003f,-0.818f),
        new Vector2(-19.735f,-2.385f),
        new Vector2(-40.98f,-32.556f),
        new Vector2(-36.01f,-30.82f),
        new Vector2(-26.02f,-30.825f),
        new Vector2(-19.74f,-32.394f),
        new Vector2(-14.675f,-30.132f)



        

        
    };

    Vector2[] roomMaxs = new Vector2[]
    {
        new Vector2(-36.12f,2.304f),
        new Vector2(-19.856f,0.818f),
        new Vector2(-14.779f,2.335f),
        new Vector2(-36.115f,-27.688f),
        new Vector2(-28.589f,-29.184f),
        new Vector2(-19.841f,-29.183f),
        new Vector2(-14.776f,-27.665f),
        new Vector2(-13.375f,29.679f)


       
    };


//public RoomBounds roomBounds;

    void Start() {
        DrawDistractionToggle = gameObject.GetComponent<Toggle>();

    //     roomBounds = GetComponent<RoomBounds>();
    //     if (roomBounds != null) {
    //     roomMin = roomBounds.GetRoomMin();
    //     roomMax = roomBounds.GetRoomMax();
    // } else {
    //     Debug.LogError("RoomBounds component not found on this object.");
    // }
    }

    void Update() {
        if (!MouseOverUILayerObject.IsPointerOverUIObject() && DrawDistractionToggle.isOn && !distractionExist && Input.GetMouseButtonDown(0)) {
            Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spawnPosition.z = 0;

            if(CheckSpawnPosition(spawnPosition)){

            GameObject newDistraction = Instantiate(distractionPrefab, spawnPosition, Quaternion.identity);

            distractionExist = true;
            DrawDistractionToggle.isOn = false;

            for (int i = 0; i < ghosts.Length; i++) {
                ghosts[i].GetComponent<GhostMovement>().setNewDistraction(newDistraction);
                ghosts[i].GetComponent<GhostMovement>().setDistractionExist(distractionExist);
            }
                
            //FindAnyObjectByType<ghost2>().objectToThrow = newDistraction;

            StartCoroutine(DestroySquareAfterTime(newDistraction));
            }
        }
    }

    IEnumerator DestroySquareAfterTime(GameObject newDistraction)
    {
        yield return new WaitForSeconds(distractionLifetime);
        Destroy(newDistraction);
        distractionExist = false;

        for (int i = 0; i < ghosts.Length; i++) {
            ghosts[i].GetComponent<GhostMovement>().setDistractionExist(distractionExist);
        }
    }
    

 public bool CheckSpawnPosition(Vector3 spawnPosition)
{

    
    for (int i = 0; i < roomMins.Length; i++)
    {
        
        if (spawnPosition.x >= roomMins[i].x && spawnPosition.x <= roomMaxs[i].x &&
            spawnPosition.y >= roomMins[i].y && spawnPosition.y <= roomMaxs[i].y)
        {
            return true; 
        }
        
    }

    return false; 
}
}