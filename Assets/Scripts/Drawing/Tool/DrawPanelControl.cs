using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawPanelControl : MonoBehaviour
{
    private GameObject drawingPanel;
    private Toggle DrawToolToggle;

    void Start()
    {
        drawingPanel = Camera.main.gameObject.transform.Find("DrawPanel").gameObject;
        DrawToolToggle = gameObject.GetComponent<Toggle>();
    }

    public void drawToolsOnClick()
    {
        drawingPanel.SetActive(DrawToolToggle.isOn);
    }
}