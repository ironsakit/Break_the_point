using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GamaIsPaused = false;
    public GameObject MenuPause, info, volume;

    void Update()
    {   
        if (Input.GetKeyDown(KeyCode.Escape))
        {   
            if (GamaIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (info.name == "Info"){
                Info();
            }
        }
    }

    public void EnterVolumeSetting()
    {
        MenuPause.SetActive(false);
        volume.SetActive(true);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ExitVolumeSetting()
    {
        MenuPause.SetActive(true);
        volume.SetActive(false);
    }

    public void Info()
    {
        if (!info.activeSelf)
        {
            info.SetActive(true);
        }
        else
        {
            info.SetActive(false);
        }
    }

    public void FermaTempo()
    {
        Time.timeScale = 0f;  // fermiamo il tempo
    }

    public void RicominciaTempo()
    {
        Time.timeScale = 1f;  // Facciamo ripartire il tempo
    }

    public void Pause()
    {
        if (info.name == "Info")
        {
            info.SetActive(true);
        }
        MenuPause.SetActive(true);  // Attiviamo il menu
        FermaTempo();   
        GamaIsPaused = true;
        // Mostra il cursore
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Resume()
    {
        if (info.name == "Info")
        {
            Info();
        }
        volume.SetActive(false);
        MenuPause.SetActive(false);  // Disattiviamo il menu
        RicominciaTempo();
        GamaIsPaused = false;
        // Nasconde il cursore
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
