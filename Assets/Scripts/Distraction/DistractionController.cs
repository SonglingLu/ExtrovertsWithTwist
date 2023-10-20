using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractionController : MonoBehaviour
{
    public GameObject squarePrefab;
    public int maxSquares = 3;
    private int squareCount = 0;
    private int clickTimes = 0;

    public float squareLifetime = 5.0f; // 正方形的生命周期（秒）

    void Update()
    {
        if (clickTimes<maxSquares && squareCount < maxSquares && Input.GetMouseButtonDown(0))
        {
            Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spawnPosition.z = 0;
            GameObject newSquare = Instantiate(squarePrefab, spawnPosition, Quaternion.identity);
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