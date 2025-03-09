using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class menuScript : MonoBehaviour
{

    public GameObject menu, volume;

    public void Avvia()
    {   
        StopAllSounds();
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void EnterVolumeSetting() { menu.SetActive(false); volume.SetActive(true); }
    public void ExitVolumeSetting() { menu.SetActive(true); volume.SetActive(false); }

    public void StopAllSounds()
    {
        AudioSource[] allAudioSources = FindObjectsByType<AudioSource>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.Stop(); // Ferma ogni suono attivo
        }
    }
}
