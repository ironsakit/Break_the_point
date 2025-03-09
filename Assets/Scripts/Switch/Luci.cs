using System;
using System.Collections;
using UnityEngine;

public class Luci : MonoBehaviour
{

    public Light[] SpotLights;
    public GameObject[] cylinder;
    float tempo = 0;
    public int livello7;

    // Update is called once per frame
    void Update()
    {
        // Aumenta il tempo ad ogni frame
        tempo += Time.deltaTime;

        if (livello7 == 1)
        {
            if (tempo >= 0.2f)
            {
                tempo = 0f; // Reset del timer per ricominciare il cicl
                StartCoroutine(cambiaIntensita2());
            }
        }
        else if (livello7 == 0)
        {
            if (tempo >= 1f)
            {
                tempo = 0f; // Reset del timer per ricominciare il cicl
                StartCoroutine(cambiaIntensita());
            }
        }
    }

    public IEnumerator cambiaIntensita()
    {
        int randomInt = UnityEngine.Random.Range(0, SpotLights.Length);

        for (int i = 0; i < SpotLights.Length; i++)
        {
            if (i == randomInt)
            {
                SpotLights[i].intensity = 0;
                yield return new WaitForSeconds(1.4f);
                SpotLights[i].intensity = 133.95f;
            }
        }
    }
    public IEnumerator cambiaIntensita2()
    {
        for (int i = 0; i < SpotLights.Length; i++)
        {
            SpotLights[i].intensity = 0;
            yield return new WaitForSeconds(0.01f);
            SpotLights[i].intensity = 133.95f;
        }
    }
    public IEnumerator cambiaIntensita3()
    {
        for (int i = 0; i < 6; i++)
        {
            SpotLights[0].intensity = 0;
            yield return new WaitForSeconds(0.18f);
            SpotLights[0].intensity = 133.95f;
            yield return new WaitForSeconds(0.18f);
        }
    }

    public void SpegniTutto()
    {
        for (int i = 0; i < SpotLights.Length; i++)
        {
            SpotLights[i].intensity = 0;
            cylinder[i].SetActive(false);
        }
    }
}
