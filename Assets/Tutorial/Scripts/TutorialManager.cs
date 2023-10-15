using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    
    int currentStep=0;
    public List<TutorialStep> steps = new List<TutorialStep>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNextStep()
    {
        
        foreach (Substep step in steps[currentStep].listObjects)
        {
            
            step.obj.SetActive(!step.makeDisappear);
                
            
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