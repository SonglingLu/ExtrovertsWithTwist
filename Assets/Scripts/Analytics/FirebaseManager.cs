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

    public int equipmentAverageInk;
    public int barrierAverageInk;
    public int distractionAverageInk;
    public int eraseAverageInk;
    public int holeAverageInk;
    public int invisibleAverageInk;

    public int averageTotalInkUsed;



}

public class FirebaseManager : MonoBehaviour
{
    [HideInInspector]
    public bool playerKilled = false;

    public string projectID = "extrovertswithtwist-default-rtdb";
    public bool recordData = true;

    InkManagement inkManager;
    // Start is called before the first frame update
    private void Start()
    {
        inkManager = FindAnyObjectByType<InkManagement>();
        StartCoroutine(GameObject.FindAnyObjectByType<FirebaseManager>().postLevelCompletionAnalytics(false));

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator postLevelCompletionAnalytics(bool levelCompleted)
    {

        if(recordData)
        {
            RestClient.Get("https://extrovertswithtwist-default-rtdb.firebaseio.com/Levels/" + "Level" + (Int32.Parse(SceneManager.GetActiveScene().name.Split(' ').Last())).ToString() + ".json").Then(response => {

                Debug.Log("Response : " + response.Text);
                if (string.IsNullOrEmpty(response.Text) || response.Text == "{}" || response.Text == "null" || response == null)
                {
                    // Handle the case where there is no data available
                    Debug.Log("No data available for " + SceneManager.GetActiveScene().name);

                    LevelData updatedLevelData = new LevelData();

                    if (levelCompleted)
                    {
                        updatedLevelData.completeCount = 1;
                    }
                    else
                    {
                        updatedLevelData.completeCount = 0;
                        updatedLevelData.levelOpenedCount = 1;


                        updatedLevelData.totalGhostKills = 0;
                        updatedLevelData.totalGhostEscape = 0;

                    }


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
                    if (levelCompleted)
                    {
                        updatedLevelData.completeCount = levelData.completeCount + 1;
                    }
                    else
                    {
                        updatedLevelData.levelOpenedCount = levelData.levelOpenedCount + 1;
                    }






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







    public IEnumerator updateGhostAnalytics(bool escaped)
    {
        if (recordData)
        {
            RestClient.Get("https://extrovertswithtwist-default-rtdb.firebaseio.com/Levels/" + "Level" + (Int32.Parse(SceneManager.GetActiveScene().name.Split(' ').Last())).ToString() + ".json").Then(response => {

                Debug.Log("Response : " + response.Text);
                if (string.IsNullOrEmpty(response.Text) || response.Text == "{}" || response.Text == "null" || response == null)
                {
                    // Handle the case where there is no data available
                    Debug.Log("No data available for " + SceneManager.GetActiveScene().name);

                    LevelData updatedLevelData = new LevelData();


                    updatedLevelData.totalGhostKills = 0;
                    updatedLevelData.totalGhostEscape = 0;

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



                    if (escaped)
                    {
                        updatedLevelData.totalGhostEscape = levelData.totalGhostEscape + 1;

                        updatedLevelData.totalGhostKills = levelData.totalGhostKills;

                    }
                    else if (!escaped)
                    {
                        updatedLevelData.totalGhostKills = levelData.totalGhostKills + 1;
                        updatedLevelData.totalGhostEscape = levelData.totalGhostEscape;

                    }


                    RestClient.Put("https://extrovertswithtwist-default-rtdb.firebaseio.com/Levels/" + "Level" + (Int32.Parse(SceneManager.GetActiveScene().name.Split(' ').Last())).ToString() + ".json", updatedLevelData).Then(response =>
                    {
                        Debug.Log("Updated Level count successfully");
                    });


                }



            }).Catch(error => {
                Debug.LogError("Error: " + error.Message);
            }); 

            yield break;


        }
    }

    public IEnumerator updateInkAnalytics()
    {
        if (!inkDataPosted && inkManager!= null)
        {
            RestClient.Get("https://extrovertswithtwist-default-rtdb.firebaseio.com/Levels/" + "Level" + (Int32.Parse(SceneManager.GetActiveScene().name.Split(' ').Last())).ToString() + ".json").Then(response => {

                
                    // Deserialize the JSON into a LevelData object
                    LevelData levelData = JsonUtility.FromJson<LevelData>(response.Text);

                    Debug.Log(SceneManager.GetActiveScene().name + " exists in database");

                    LevelData updatedLevelData = new LevelData();



                    updatedLevelData = levelData;

             



                updatedLevelData.equipmentAverageInk = (int)((inkManager.inkUsage[0] + (levelData.equipmentAverageInk * (levelData.levelOpenedCount - 1))) / (levelData.levelOpenedCount));

                updatedLevelData.barrierAverageInk = (int)((inkManager.inkUsage[1] + (levelData.barrierAverageInk * (levelData.levelOpenedCount - 1))) / (levelData.levelOpenedCount));

                updatedLevelData.distractionAverageInk = (int)((inkManager.inkUsage[2] + (levelData.distractionAverageInk * (levelData.levelOpenedCount - 1))) / (levelData.levelOpenedCount));

                updatedLevelData.eraseAverageInk = (int)((inkManager.inkUsage[3] + (levelData.eraseAverageInk * (levelData.levelOpenedCount - 1))) / (levelData.levelOpenedCount));

                updatedLevelData.holeAverageInk = (int)((inkManager.inkUsage[4] + (levelData.holeAverageInk * (levelData.levelOpenedCount - 1))) / (levelData.levelOpenedCount));

                updatedLevelData.invisibleAverageInk = (int)((inkManager.inkUsage[5] + (levelData.invisibleAverageInk * (levelData.levelOpenedCount - 1))) / (levelData.levelOpenedCount));


                updatedLevelData.averageTotalInkUsed = (int)((inkManager.ink + (levelData.averageTotalInkUsed * (levelData.levelOpenedCount -1))) / (levelData.levelOpenedCount));


                    RestClient.Put("https://extrovertswithtwist-default-rtdb.firebaseio.com/Levels/" + "Level" + (Int32.Parse(SceneManager.GetActiveScene().name.Split(' ').Last())).ToString() + ".json", updatedLevelData).Then(response =>
                        {
                            Debug.Log("Updated ink Data successfully");
                            inkDataPosted = true;
                        }).Catch(error => {
                            Debug.LogError("Error: " + error.Message);
                        });






            }).Catch(error => {
                Debug.LogError("Error: " + error.Message);
            }); ;

            yield break;


        }
    }

    bool inkDataPosted = false;
    


}
