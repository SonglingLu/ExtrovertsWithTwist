using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public GameObject DrawTool;
    Toggle DrawToolToggle;
    bool firstToggle = true;

    //public GameObject BarrierTool;
    //Toggle BarrierToolToggle;
    //bool SecondToggle = true;

    public int currentStep=0;
    public List<TutorialStep> steps = new List<TutorialStep>();
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Level 0") {

            DrawToolToggle = DrawTool.GetComponent<Toggle>();
            //BarrierToolToggle = BarrierTool.GetComponent<Toggle>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Level 0")
        {

            if (firstToggle && DrawToolToggle.isOn)
            {
                LoadNextStep();
                firstToggle = false;
            }
            //if (SecondToggle && BarrierToolToggle.isOn)
            //{
            //    //LoadNextStep();
            //    SecondToggle = false;

            //}
        }

    }

    public void LoadNextStep()
    {
        if(currentStep != -1)
        {
            foreach (Substep step in steps[currentStep].listObjects)
            {

                step.obj.SetActive(!step.makeDisappear);


            }


        }
        
       
        

        currentStep++;
         Debug.Log(currentStep);
        if (currentStep<steps.Count) {

            foreach (Substep step in steps[currentStep].listObjects)
            {
                step.obj.SetActive(true);

            }


        }

       
        


        
    }
    public void CloseTutorial()
    {
        foreach (Substep step in steps[currentStep].listObjects)
        {

            step.obj.SetActive(!step.makeDisappear);


        }
    }

}

[System.Serializable]
public class TutorialStep
{
    public List<Substep> listObjects;
}

[System.Serializable]
public class Substep
{
    public GameObject obj;
    public bool makeDisappear;
}