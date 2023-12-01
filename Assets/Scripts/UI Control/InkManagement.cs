using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InkManagement : MonoBehaviour
{
    [HideInInspector]
    public float ink;
    public GameObject finish_panel;
    public GameObject ink_panel;

    public Image inkBar;

    public List<float> inkUsage;

    // Start is called before the first frame update
    void Start()
    {
        ink = 100f;
        inkUsage = new List<float>() {0, 0, 0, 0, 0, 0}; //Tool, Barrier, Distraction,erase,hole, invisible
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UseInk(float amount, int type) {
        if (amount <= ink) {
            ink -= amount;
            inkUsage[type] += amount;
        } else {
            inkUsage[type] += ink;
            ink = 0;
        }
        
        inkBar.fillAmount = ink / 100f;
        if (ink <= 0)
        {
            finish_panel.SetActive(true);
            ink_panel.SetActive(true);
            GlobalVariables.SetPlayerMovable(false);
        }
    }

    public float GetInk() {
        return ink;
    }


}
