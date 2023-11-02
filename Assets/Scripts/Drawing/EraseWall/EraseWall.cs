using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class EraseWall : MonoBehaviour
{
    [SerializeField] LayerMask[] layers;
    private Toggle EraseWallToggle;
    private bool erasing = false;
    private List<Vector2> FingerPositions;
    private List<GameObject> ErasedWall;
    private float eraseDuration = 8f;

    private float inkCoeff = 1f;
    public GameObject InkBar;

    // Start is called before the first frame update
    void Start()
    {
        EraseWallToggle = gameObject.GetComponent<Toggle>();
        FingerPositions = new List<Vector2>();
        ErasedWall = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
         // check if button is on
        if (EraseWallToggle.isOn) {
            // Check for mouse button press to start drawing a line
            if (Input.GetMouseButtonDown(0) && InkBar.GetComponent<InkManagement>().GetInk() > 0) {
                FingerPositions.Clear();
                ErasedWall.Clear();
                erasing = true;

                FingerPositions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
            // Check for mouse button hold to continue drawing the line
            if (erasing && InkBar.GetComponent<InkManagement>().GetInk() > 0 && Input.GetMouseButton(0)) {
                Vector2 temFingerPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (Vector2.Distance(temFingerPos, FingerPositions[FingerPositions.Count - 1]) > 0.01f)
                {
                    float distance = Vector2.Distance(temFingerPos, FingerPositions.Last());
                    InkBar.GetComponent<InkManagement>().UseInk(distance * inkCoeff, 3);
                    AddFingerPos(temFingerPos);
                }
            }
            if (erasing && Input.GetMouseButtonUp(0)) {
                FinalizeErase(new List<GameObject>(ErasedWall));

                EraseWallToggle.isOn = false;
                erasing = false;
            }
            
        }
    }

    private void AddFingerPos(Vector2 newFingerPos) {
        FingerPositions.Add(newFingerPos);

        Collider2D[] wallColliders = Physics2D.OverlapCircleAll(newFingerPos, 0.1f, layers[0]);

        if (wallColliders != null) {

            foreach (Collider2D wallCollider in wallColliders) {
                GameObject wall = wallCollider.gameObject;

                if (!ErasedWall.Contains(wall)) {
                    wall.GetComponent<SpriteRenderer>().color = new Color32(255, 209, 0, 100);
                    ErasedWall.Add(wall);
                }
            }
        }


        Collider2D[] barrierColliders = Physics2D.OverlapCircleAll(newFingerPos, 0.1f, layers[1]);

        if (barrierColliders != null) {

            foreach (Collider2D barrierCollider in barrierColliders) {
                GameObject barrier = barrierCollider.gameObject;

                Destroy(barrier);
            }
        }
            
    }

    private void FinalizeErase(List<GameObject> walls) {
        foreach (GameObject wall in walls) {
            wall.SetActive(false);
        }

        StartCoroutine(ReactivateWall(walls, eraseDuration));
    }

    IEnumerator ReactivateWall(List<GameObject> walls, float delay)
    {
        yield return new WaitForSeconds(delay);
        foreach (GameObject wall in walls) {
            wall.SetActive(true);
            wall.GetComponent<SpriteRenderer>().color = new Color32(255, 209, 0, 255); 
        }
    }
}
