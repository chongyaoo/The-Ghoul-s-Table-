using UnityEngine;
using System.Collections;
using UnityEditor;


public class LightFlicker : MonoBehaviour
{

    private Light dirLight;

    private float onTime1;
    private float offTime1;

    private float onTime2;
    private float offTime2;

    void Start()
    {
        dirLight = GetComponent<Light>();
        dirLight.enabled = true;
        StartCoroutine(Flickering());
    }

    private IEnumerator Flickering()
    {
        while (true)
        {
            dirLight.enabled = true;
            onTime1 = UnityEngine.Random.value;
            yield return new WaitForSeconds(onTime1);

            dirLight.enabled = false;
            offTime1 = UnityEngine.Random.value;
            yield return new WaitForSeconds(offTime1);

            dirLight.enabled = true;
            onTime2 = UnityEngine.Random.value/2;
            yield return new WaitForSeconds(onTime2);

            dirLight.enabled = false;
            offTime2 = UnityEngine.Random.value/2;
            yield return new WaitForSeconds(offTime2);
        }
    }
}
