using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InkManagement : MonoBehaviour
{
    private float ink;
    public Image inkBar;

    private List<float> inkUsage;

    // Start is called before the first frame update
    void Start()
    {
        ink = 100f;
        inkUsage = new List<float>() {0, 0, 0, 0, 0, 0};
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
    }

    public float GetInk() {
        return ink;
    }
}
