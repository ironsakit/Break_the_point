using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    [SerializeField] private AudioSource soundFXObject;
    private AudioSource audioSource;

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

    public AudioSource PlayMusic(AudioClip audioClip, Transform spawnTransform, float volume)
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
        return audioSource;
    }

    public void StopMusic()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            DestroyMusic();
        }
    }

    public void DestroyMusic()
    {
        audioSource.Stop();
        Destroy(audioSource);
    }
}
