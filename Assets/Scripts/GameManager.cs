using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Corridoio;
    public GameObject CorridoioConnesso;
    private Vector3 posizioneCorridoio;
    private Vector3 rotazioneCorridoio;
    int index = 0;

    private List<GameObject> corridoi = new List<GameObject>();

    void Start()
    {
        // Inizializza la posizione del primo corridoio
        posizioneCorridoio = new Vector3(22.4427f, 0f, 21.48035f);  // Coordinate della stanza principale
        rotazioneCorridoio = new Vector3(-90f, 0f, 0f);  // Rotazione iniziale del corridoio
        GameObject gameObject = Instantiate(Corridoio, posizioneCorridoio, Quaternion.Euler(rotazioneCorridoio));
        corridoi.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        // Assicurati che ci siano almeno 3 corridoi prima di tentare di rimuovere uno
        if (corridoi.Count > 3)
        {
            Destroy(corridoi[0]);  // Distruggi il corridoio più vecchio
            corridoi.RemoveAt(0);  // Rimuovilo dalla lista
        }
    }

    public void function(string colliderName)
    {
        GameObject nuovoCorridoio = null;
        if (colliderName == "GenerateRoomDestra")
        {
            GameObject spawnerDestro = corridoi.ElementAt(corridoi.Count-1).transform.Find("SpawnerDestro")?.gameObject;
            posizioneCorridoio = spawnerDestro.transform.position;
            rotazioneCorridoio += new Vector3(0f, 90f, 0f); // Ruota di 90° per ogni nuovo corridoio
            nuovoCorridoio = Instantiate(CorridoioConnesso, posizioneCorridoio, Quaternion.Euler(rotazioneCorridoio));
            corridoi.Add(nuovoCorridoio);
        }
        else if (colliderName == "GenerateRoomSinistra")
        {
            GameObject spawnerSinistro = corridoi.ElementAt(corridoi.Count - 1).transform.Find("SpawnerSinistro")?.gameObject;
            posizioneCorridoio = spawnerSinistro.transform.position;
            rotazioneCorridoio += new Vector3(0f, -90f, 0f); // Ruota di 90° per ogni nuovo corridoio
            nuovoCorridoio = Instantiate(CorridoioConnesso, posizioneCorridoio, Quaternion.Euler(rotazioneCorridoio));
            corridoi.Add(nuovoCorridoio);
        }
        else if (colliderName == "DestroyRoomDestra")
        {
            StartCoroutine(Wait());
        }
        else if (colliderName == "DestroyRoomSinistra")
        {
            StartCoroutine(Wait2());
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(corridoi[corridoi.Count - 1]);  // Distruggi il corridoio più vecchio
        corridoi.RemoveAt(corridoi.Count - 1);  // Rimuovilo dalla lista
        GameObject spawnerDestro = corridoi.ElementAt(corridoi.Count - 1).transform.Find("SpawnerDestro")?.gameObject;
        posizioneCorridoio = spawnerDestro.transform.position;
        rotazioneCorridoio -= new Vector3(0f, 90f, 0f); // Ruota di 90° per ogni nuovo corridoio
    }

    IEnumerator Wait2()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(corridoi[corridoi.Count - 1]);  // Distruggi il corridoio più vecchio
        corridoi.RemoveAt(corridoi.Count - 1);  // Rimuovilo dalla lista
        GameObject spawnerSinistro = corridoi.ElementAt(corridoi.Count - 1).transform.Find("SpawnerSinistro")?.gameObject;
        posizioneCorridoio = spawnerSinistro.transform.position;
        rotazioneCorridoio -= new Vector3(0f, -90f, 0f); // Ruota di 90° per ogni nuovo corridoio
    }
}
