using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostColliderScript : MonoBehaviour
{
    public bool checkComplete;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (SceneManager.GetActiveScene().name == "Level 1")
        {
            if (collision.gameObject.CompareTag("Ghost") && !checkComplete)
            {
                TutorialManager myScriptInstance = FindObjectOfType<TutorialManager>();
                if (myScriptInstance != null)
                {
                    myScriptInstance.LoadNextStep();
                    Destroy(gameObject);
                }



            }
            else if (collision.gameObject.CompareTag("Player") && checkComplete)
            {
                TutorialManager myScriptInstance = FindObjectOfType<TutorialManager>();
                if (myScriptInstance != null)
                {
                    myScriptInstance.CloseTutorial();
                    Destroy(gameObject);
                }


            }

        }

        else if (SceneManager.GetActiveScene().name == "Level 2")
        {
            if (collision.gameObject.CompareTag("Player") && !checkComplete)
            {
                TutorialManager myScriptInstance = FindObjectOfType<TutorialManager>();
                if (myScriptInstance != null)
                {
                    myScriptInstance.LoadNextStep();
                    Destroy(gameObject);
                }



            }
            else if (collision.gameObject.CompareTag("Ghost") && checkComplete)
            {
                TutorialManager myScriptInstance = FindObjectOfType<TutorialManager>();
                if (myScriptInstance != null)
                {
                    myScriptInstance.CloseTutorial();
                    Destroy(gameObject);
                }


            }

        }




    }
}
