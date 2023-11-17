using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawShortcutKey : MonoBehaviour
{
    public Toggle DrawToolToggle;
    public Toggle DrawBarrierToggle;
    public Toggle DrawDistractionToggle;
    public Toggle EraseWallToggle;
    public Toggle DrawHoleToggle;
    public Toggle InvisibleBrushToggle;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!GlobalVariables.GetDrawing() && Input.GetKeyDown(KeyCode.Alpha1)) {
            DrawToolToggle.isOn = !DrawToolToggle.isOn;
        }
        else if (!GlobalVariables.GetDrawing() && Input.GetKeyDown(KeyCode.Alpha2)) {
            DrawBarrierToggle.isOn = !DrawBarrierToggle.isOn;
        }
        else if (!GlobalVariables.GetDrawing() && Input.GetKeyDown(KeyCode.Alpha3)) {
            DrawDistractionToggle.isOn = !DrawDistractionToggle.isOn;
        }
        else if (!GlobalVariables.GetDrawing() && Input.GetKeyDown(KeyCode.Alpha4)) {
            EraseWallToggle.isOn = !EraseWallToggle.isOn;
        }
        else if (!GlobalVariables.GetDrawing() && Input.GetKeyDown(KeyCode.Alpha5)) {
            DrawHoleToggle.isOn = !DrawHoleToggle.isOn;
        }
        else if (!GlobalVariables.GetDrawing() && Input.GetKeyDown(KeyCode.Alpha6)) {
            InvisibleBrushToggle.isOn = !InvisibleBrushToggle.isOn;
        }
    }
}
