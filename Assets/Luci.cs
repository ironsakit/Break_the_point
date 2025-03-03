using System;
using System.Collections;
using UnityEngine;

public class Luci : MonoBehaviour
{

    public Light[] SpotLights;
    float tempo = 0;
    private bool isCoroutineRunning = false;

    // Update is called once per frame
    void Update()
    {
        // Aumenta il tempo ad ogni frame
        tempo += Time.deltaTime;

        // Se è passato più di 3 secondi e la coroutine non è già in esecuzione, la avvia
        if (tempo >= 3f && !isCoroutineRunning)
        {
            tempo = 0f; // Reset del timer per ricominciare il ciclo
            StartCoroutine(cambiaIntensita());
        }
    }

    IEnumerator cambiaIntensita()
    {
        isCoroutineRunning = true;
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
        isCoroutineRunning = false;
    }
}
