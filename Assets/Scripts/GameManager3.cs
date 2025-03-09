using UnityEngine;

public class GameManager3 : MonoBehaviour
{

    public AudioClip musica;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MusicManager.instance.PlayMusic(musica, transform, 1f);
    }
}
