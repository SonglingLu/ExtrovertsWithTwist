using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractionController : MonoBehaviour
{
    public GameObject squarePrefab;
    public int maxSquaresExist = 1;
    private int squareCount = 0;
    private int clickTimes = 0;
    private int maxClickTimes = 3;

    public float squareLifetime = 5.0f; // 正方形的生命周期（秒）

    void Update()
    {
        if (clickTimes< maxClickTimes && squareCount < maxSquaresExist && Input.GetMouseButtonDown(0))
        {
            Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spawnPosition.z = 0;
            GameObject newSquare = Instantiate(squarePrefab, spawnPosition, Quaternion.identity);
            FindAnyObjectByType<ghost2>().objectToThrow = newSquare;
            StartCoroutine(DestroySquareAfterTime(newSquare));
            squareCount++;
            clickTimes++;
        }
    }

    IEnumerator DestroySquareAfterTime(GameObject square)
    {
        yield return new WaitForSeconds(squareLifetime);
        Destroy(square);
        squareCount--;
    }
}