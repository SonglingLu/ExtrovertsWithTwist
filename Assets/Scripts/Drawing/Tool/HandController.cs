using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class HandController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public bool buttonPressed;
    
    private Vector3 offsetToMouse;
    public GameObject LeftArmSolver, RightArmSolver;

    Vector3 defaltpositionLeft, defaltpositionRight;
    public void OnPointerDown(PointerEventData eventData)
    {
        
        defaltpositionLeft = LeftArmSolver.transform.localPosition;
        defaltpositionRight = RightArmSolver.transform.localPosition;
        buttonPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonPressed = false;
        ResetHandPos();
    }

    private void Update()
    {
        if (buttonPressed)
        {
            
            
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
           
            RightArmSolver.transform.position = mousePosition;
            
        }
    }
    public void ResetHandPos()
    {
        LeftArmSolver.transform.localPosition = defaltpositionLeft;

        RightArmSolver.transform.localPosition = defaltpositionRight;
    }
}