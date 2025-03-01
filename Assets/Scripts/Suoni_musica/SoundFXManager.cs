using UnityEngine;
using UnityEngine.Audio;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;

    [SerializeField] private AudioSource soundFXObject;
    private AudioSource audioSource;
    private float timePaused = 0f;
    private bool isPaused = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        if (audioClip == null)
        {
            Debug.LogWarning("AudioClip nullo, impossibile riprodurre il suono.");
            return;
        }

        if (soundFXObject == null)
        {
            Debug.LogError("soundFXPrefab non assegnato! Controlla l'Inspector.");
            return;
        }

        // Instanzia un nuovo AudioSource identico al prefab
        AudioSource newAudioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        // Configura l'AudioSource con il clip specificato
        newAudioSource.clip = audioClip;
        newAudioSource.volume = volume;
        newAudioSource.Play();

        // Distruggi l'oggetto dopo la durata del suono
        Destroy(newAudioSource.gameObject, audioClip.length);
    }

    public void StopStartSoundFXClip()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            // Memorizza il tempo della traccia e mettila in pausa
            timePaused = audioSource.time;
            audioSource.Pause();
            isPaused = true;
        }
        else if(isPaused)
        {
            audioSource.time = timePaused;
            //PLay the sound
            audioSource.Play();
            isPaused = false;
        }
    }
}
