using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioScena : MonoBehaviour
{
    public bool cambioScena = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cambioScena = true;
            SceneManager.LoadScene(3);
        }
    }
}

