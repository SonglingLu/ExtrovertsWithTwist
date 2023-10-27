using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleMechanic : MonoBehaviour
{
    public bool isCloaked = false;
    private GhostMovement ghostMovement;
    private float cloakDuration = 5f;


    // Start is called before the first frame update
    void Start()
    {

        ghostMovement = FindObjectOfType<GhostMovement>();
        if (ghostMovement == null)
        {
            Debug.LogError("GhostChase script not found in the scene. Please ensure a ghost with the GhostChase script is present.");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !isCloaked){
            Debug.Log("Cloaking Activated");
            StartCoroutine(ActivateCloaking());
        }
        
    }

    private IEnumerator ActivateCloaking()
    {
        isCloaked = true;

        yield return new WaitForSeconds(5);

        isCloaked = false;
        Debug.Log("Cloaking Deactivated");

    }

    public bool IsPlayerCloaked()
    {
        return isCloaked;
    }
}
