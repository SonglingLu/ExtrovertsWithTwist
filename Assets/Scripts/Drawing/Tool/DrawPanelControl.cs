using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawPanelControl : MonoBehaviour
{
    private GameObject drawingPanel;
    private Toggle DrawToolToggle;
    public GameObject RemoveEqButton;
    [HideInInspector]
    public DrawTool DrawToolInstace;

    void Start()
    {
        drawingPanel = Camera.main.gameObject.transform.Find("DrawPanel").gameObject;
        DrawToolToggle = gameObject.GetComponent<Toggle>();

        DrawToolInstace = drawingPanel.transform.Find("DrawBoard").transform.GetComponent<DrawTool>();
        DrawToolInstace.RemoveButton = RemoveEqButton;
    }

    public void drawToolsOnClick()
    {
        drawingPanel.SetActive(DrawToolToggle.isOn);
    }

    public void RemoveEquipmentOnClick()
    {
        DrawToolInstace.RemoveEquipment();
    }
}