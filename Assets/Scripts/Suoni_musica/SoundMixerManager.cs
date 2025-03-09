using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{

    [SerializeField] private AudioMixer audioMixer;

    private GameObject player;
    private PlayerMovement playerMovement;

    public void SetMasterVolume(float level)
    {
        float volume = Mathf.Pow(10, level / 20); // Conversione logaritmica
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }

    public void SetSoundFXVolume(float level)
    {
        float volume = Mathf.Pow(10, level / 20); // Conversione logaritmica
        audioMixer.SetFloat("SoundFXVolume", Mathf.Log10(volume) * 20);
    }
    public void SetMusicVolume(float level)
    {
        float volume = Mathf.Pow(10, level / 20); // Conversione logaritmica
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }

    public void SetSensibility(float level)
    {
        player = GameObject.Find("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.mouseSensitivity = level;
    }
}
