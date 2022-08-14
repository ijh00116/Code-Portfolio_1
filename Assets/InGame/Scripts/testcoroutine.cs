using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testcoroutine : MonoBehaviour
{
    Coroutine asas;
    // Start is called before the first frame update
    void Start()
    {
        asas=StartCoroutine(test());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            StopCoroutine(asas);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            asas = StartCoroutine(test());
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            StopAllCoroutines();
        }
    }

    IEnumerator test()
    {
        while(true)
        {
            Debug.Log("Ω√¿€");

            yield return new WaitForSeconds(2);
            Debug.Log("≥°");
            yield return new WaitForSeconds(2);
        }
       
    }
}
