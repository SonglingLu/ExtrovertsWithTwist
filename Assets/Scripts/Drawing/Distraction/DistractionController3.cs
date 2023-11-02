using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DistractionController3 : MonoBehaviour
{
    public GameObject distractionPrefab;
    private bool distractionExist = false;
    private float distractionLifetime = 5.0f;

    private Toggle DrawDistractionToggle;

    [SerializeField] GameObject[] ghosts;

    Vector2[] roomMins = new Vector2[]
    {
        new Vector2(-21.38f,-2.53f),
        new Vector2(-15.91f,-1.15f),
        new Vector2(-2.22f,-2.27f),
        new Vector2(-14.43f,-6.02f),
        new Vector2(-12.219f,-6.029f),
        new Vector2(-0.953f,-3.785f),
        new Vector2(2.41f,-0.01f),
        new Vector2(3.511f,-0.5f),

        

        
    };

    Vector2[] roomMaxs = new Vector2[]
    {
        new Vector2(-16.029f,2.342f),
        new Vector2(-2.346f,1.038f),
        new Vector2(2.29f,2.005f),
        new Vector2(-12.26f,-1.29f),
        new Vector2(1.237f,-3.837f),
        new Vector2(1.244f,-2.399f),
        new Vector2(3.793f,0.31f),
        new Vector2(3.784f,-0.127f),

       
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