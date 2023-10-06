using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartDestroyTimer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator StartDestroyTimer()
    {
        yield return new WaitForSeconds(8f);
        //Destroy(gameObject);
    }
   }
