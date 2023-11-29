using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectHover : MonoBehaviour
{
    public SpriteRenderer sprite;
    public HoleController holeController;

    public float  defaultAlpha= 0.4f, hoverAlpha = 0.75f;

    private void Start()
    {
        
      //  holeController = FindObjectOfType<HoleController>();

        Color currentColor = sprite.color;

        currentColor.a = defaultAlpha;

        sprite.color = currentColor;

        holeController.mouseOverHoleable = false;
    }
    Vector3 mousePosition;
    private void Update()
    {
        
        
    }

    private void OnMouseEnter()
    {
        if(holeController != null && holeController.DrawHoleToggle != null)
        {
            if (holeController.DrawHoleToggle.isOn)
            {
                Color currentColor = sprite.color;

                currentColor.a = hoverAlpha;

                sprite.color = currentColor;
            }

            holeController.mouseOverHoleable = true;
        }
        

    }

    private void OnMouseExit()
    {
        Color currentColor = sprite.color;

        currentColor.a = defaultAlpha;

        sprite.color = currentColor;
        
        holeController.mouseOverHoleable = false;
    }
}
