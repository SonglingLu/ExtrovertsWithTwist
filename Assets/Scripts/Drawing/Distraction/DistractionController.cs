using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DistractionController : MonoBehaviour
{
    public GameObject distractionPrefab;
    private bool distractionExist = false;
    private float distractionLifetime = 5.0f;

    private Toggle DrawDistractionToggle;

    [SerializeField] GameObject[] ghosts;

    Vector2[] roomMins = new Vector2[]
    {
        new Vector2(-20.09f, -2.81f),
        new Vector2(-14.64f, -1.44f),
        new Vector2(-9.77f, -0.657f),
        new Vector2(-8.749f, -0.846f),
        new Vector2(-5.56f, -3.89f),
        new Vector2(0.59f, -0.84f),
        new Vector2(-1.519f, 3.942f),
        new Vector2(-4.61f, 2.663f),
        new Vector2(0.673f, 10.586f),
        new Vector2(5.47f, 9.47f),
        new Vector2(7.108f, 14.46f)
    };

    Vector2[] roomMaxs = new Vector2[]
    {
        new Vector2(-14.71f, 2.79f),
        new Vector2(-9.83f, 1.36f),
        new Vector2(-8.806f, 0.574f),
        new Vector2(0.625f, 0.754f),
        new Vector2(-3.74f, -0.94f),
        new Vector2(2.607f, 10.562f),
        new Vector2(0.623f, 5.889f),
        new Vector2(-1.564f, 7.1f),
        new Vector2(5.371f, 12.551f),
        new Vector2(10.531f, 14.403f),
        new Vector2(7.867f, 15.986f)
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

    // 遍历每一组 roomMin 和 roomMax
    for (int i = 0; i < roomMins.Length; i++)
    {
        // 检查 spawnPosition 是否在当前组的范围内
        if (spawnPosition.x >= roomMins[i].x && spawnPosition.x <= roomMaxs[i].x &&
            spawnPosition.y >= roomMins[i].y && spawnPosition.y <= roomMaxs[i].y)
        {
            return true; // 如果满足条件，返回true
        }
        
    }

    return false; // 如果所有向量组都不满足条件，返回false
}
}