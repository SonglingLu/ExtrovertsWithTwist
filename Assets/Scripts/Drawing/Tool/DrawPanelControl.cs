using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawPanelControl : MonoBehaviour
{
    public GameObject drawingPanel;
    private Toggle DrawToolToggle;

    void Start()
    {
        DrawToolToggle = gameObject.GetComponent<Toggle>();
    }

    public void drawToolsOnClick()
    {
        drawingPanel.SetActive(DrawToolToggle.isOn);
    }
}