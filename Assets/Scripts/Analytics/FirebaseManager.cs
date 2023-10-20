using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

[System.Serializable]
public class LevelData
{
    public int completeCount;
    public int levelOpenedCount;

    public int totalGhostKills;
    public int totalGhostEscape;

}

public class FirebaseManager : MonoBehaviour
{
    public string projectID = "extrovertswithtwist-default-rtdb";

    public int currentGhostKills=0;
    public int currentGhostEscapes=0;

   
    // Start is called before the first frame update
    private void Start()
    {

        StartCoroutine(GameObject.FindAnyObjectByType<FirebaseManager>().postLevelAnalytics(false,false));

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator postLevelAnalytics(bool levelCompleted, bool updateGhostTriggersOnly)
    {

        

        RestClient.Get("https://extrovertswithtwist-default-rtdb.firebaseio.com/Levels/" + "Level" + (Int32.Parse(SceneManager.GetActiveScene().name.Split(' ').Last())).ToString() + ".json").Then(response=> {

            Debug.Log("Response : " + response.Text);
            if (string.IsNullOrEmpty(response.Text) || response.Text == "{}" || response.Text == "null" || response ==null)
            {
                // Handle the case where there is no data available
                Debug.Log("No data available for " + SceneManager.GetActiveScene().name);
         
                LevelData updatedLevelData = new LevelData();
                if(!updateGhostTriggersOnly)

                {
                    if (levelCompleted)
                    {
                        updatedLevelData.completeCount = 1;
                    }
                    else
                    {
                        updatedLevelData.completeCount = 0;
                        updatedLevelData.levelOpenedCount = 1;

                    }
                }
               
                updatedLevelData.totalGhostKills = currentGhostKills;
                updatedLevelData.totalGhostEscape = currentGhostEscapes;

                RestClient.Put("https://extrovertswithtwist-default-rtdb.firebaseio.com/Levels/" + "Level" + (Int32.Parse(SceneManager.GetActiveScene().name.Split(' ').Last())).ToString() + ".json", updatedLevelData).Then(response =>
                {
                    Debug.Log("Created new level data successfully");
                });

            }
            else
            {
                // Deserialize the JSON into a LevelData object
                LevelData levelData = JsonUtility.FromJson<LevelData>(response.Text);

                Debug.Log(SceneManager.GetActiveScene().name + " exists in database");

                LevelData updatedLevelData = new LevelData();



                updatedLevelData = levelData;

                if(!updateGhostTriggersOnly)

                {
                    if (levelCompleted)
                    {
                        updatedLevelData.completeCount = levelData.completeCount + 1;
                        updatedLevelData.levelOpenedCount = levelData.levelOpenedCount;
                    }
                    else
                    {
                        updatedLevelData.completeCount = levelData.completeCount;
                        updatedLevelData.levelOpenedCount = levelData.levelOpenedCount + 1;
                    }
                }
                

                updatedLevelData.totalGhostKills = currentGhostKills + levelData.totalGhostKills;
                updatedLevelData.totalGhostEscape = currentGhostEscapes + levelData.totalGhostEscape ;

                RestClient.Put("https://extrovertswithtwist-default-rtdb.firebaseio.com/Levels/" + "Level" + (Int32.Parse(SceneManager.GetActiveScene().name.Split(' ').Last())).ToString() + ".json", updatedLevelData).Then(response =>
                {
                    Debug.Log("Updated Level count successfully");
                });


            }



        }).Catch(error => {
            Debug.LogError("Error: " + error.Message); 
        }); ;

        yield break;


    }





}
