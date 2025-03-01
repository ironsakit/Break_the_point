using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    [SerializeField] private AudioSource soundFXObject;
    private AudioSource audioSource;
    private float timePaused = 0f;
    private bool isPaused = false;

    private void Awake()  //in questo modo possiamo chiamarlo ovunque senza un GameObject
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

        //Spawn in gameObject
        audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);
        //assign audioClip
        audioSource.clip = audioClip;
        //assign volume
        audioSource.volume = volume;
        audioSource.Play();
        //get length of sound FX volume
        float clipLength = audioSource.clip.length;
        //Destroy
        Destroy(audioSource.gameObject, clipLength);
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
        else if (isPaused)
        {
            audioSource.time = timePaused;
            //PLay the sound
            audioSource.Play();
            isPaused = false;
        }
    }

    public void DestroySound()
    {
        audioSource.Stop();
        Destroy(audioSource);
    }
}
