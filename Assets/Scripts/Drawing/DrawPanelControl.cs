using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPanelControl : MonoBehaviour
{
    public GameObject drawingPanel;

    public void drawToolsOnClick()
    {
        drawingPanel.SetActive(!drawingPanel.activeInHierarchy);
    }
}