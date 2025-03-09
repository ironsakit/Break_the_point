using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerLivelloFinale : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(sequence());
    }

    private IEnumerator sequence()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }
}
