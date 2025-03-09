using UnityEngine;

public class GameManager2 : MonoBehaviour
{
    public AudioClip[] music;
    public CambioScena cambio;
    AudioSource musica;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        musica = MusicManager.instance.PlayMusic(music[0],transform, 0.3f);
    }

    private void Update()
    {
        if (cambio.cambioScena)
        {   
            musica.Stop();
        }
    }

}
