using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DoorScript;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Corridoio;
    public GameObject CorridoioConnesso;
    public GameObject CorridoioConnessoLivello10;

    private Vector3 posizioneCorridoio;
    private Vector3 rotazioneCorridoio;
    private Door doorScript2;
    private Door doorScript1;
    public int livello = 0;
    public bool Domanda = false;
    [SerializeField] private AudioClip[] Music;
    public int PrimaParte = 0, SecondaParte = 1, TerzaParte = 19, UltimaParte = 20;
    private bool SecondaMusica = false;

    private List<GameObject> corridoi = new List<GameObject>();

    String[] voiceLines = {
            "Welcome. Your choices matter. Choose wisely.",
            "Left or right? A simple choice, isn't it?",
            "Your decisions define you.",
            "Some choices have consequences. But not yet.",
            "You're doing well. Or are you?",
            "Wait... Haven't you been here before?",
            "You're making progress. But towards what?",
            "Does this feel real to you?",
            "What if the right choice is neither left nor right?",
            "You're not supposed to be here. And yet, you keep going.",
            "Why do you keep trying? There is no way out.",
            "You're not choosing. You're obeying.",
            "Does it even matter? You will return here again.",
            "The walls are closing in. Can you feel it?",
            "You don’t remember, do you? How many times have you been here?",
            "You were warned. But you never listen.",
            "This place... It was made for you.",
            "You don’t belong here. And yet, you fit so perfectly.",
            "Turn back. Oh wait, you can't."
        };

    public String[] questions = {
        "Qual è il numero successivo?",
        "Quale animale NON può volare?",
        "Quanti mesi hanno 28 giorni?",
        "Il sole è una...?",
        "Quanto fa 2+2?",
        "Quale elemento chimico è essenziale per respirare?",
        "Quale di questi è un colore primario?",
        "Quale senso viene usato per ascoltare?",
        "Se hai 6 mele e ne dai 4, quante te ne restano?",
        "Chi ha scritto 'Romeo e Giulietta'?",
        "Qual è la cosa più reale?",
        "Dove sei ora?",
        "Il tempo scorre...?",
        "Cosa significa 'uscire'?",
        "Cosa c'è dietro questa porta?",
        "Quale porta devi scegliere?",
        "Perché continui?",
        "Chi ti sta parlando?",
        "Cosa succede se smetti di giocare?",
        "Qual è la domanda iniziale?"
    };
    void Start()
    {
        // Inizializza la posizione del primo corridoio
        posizioneCorridoio = new Vector3(22.4427f, 0f, 21.48035f);  // Coordinate della stanza principale
        rotazioneCorridoio = new Vector3(-90f, 0f, 0f);  // Rotazione iniziale del corridoio
        GameObject gameObject = Instantiate(Corridoio, posizioneCorridoio, Quaternion.Euler(rotazioneCorridoio));
        corridoi.Add(gameObject);
        MusicManager.instance.PlayMusic(Music[0], transform, 0.7f);
    }

    // Update is called once per frame
    void Update()
    {   
        if(livello == 0)
        {   
            StartCoroutine(sottotitoli(voiceLines[livello], 1f));
            livello++;
        }

        // Assicurati che ci siano almeno 3 corridoi prima di tentare di rimuovere uno
        if (corridoi.Count > 3)
        {   
            Destroy(corridoi[0]);  // Distruggi il corridoio più vecchio
            corridoi.RemoveAt(0);  // Rimuovilo dalla lista
        }
    }

    IEnumerator sottotitoli(string text, float waitSeconds)
    {
        yield return new WaitForSeconds(waitSeconds);
        yield return SubtitleManager.instance.PlaySubtitle(text, 1f);
    }

    private void CreaStanza(GameObject nuovoCorridoio)
    {
        if (livello <= 1)
        {
            nuovoCorridoio = Instantiate(CorridoioConnesso, posizioneCorridoio, Quaternion.Euler(rotazioneCorridoio));
        }
        else if (livello >= 2 && livello <= SecondaParte)
        {
            nuovoCorridoio = Instantiate(CorridoioConnessoLivello10, posizioneCorridoio, Quaternion.Euler(rotazioneCorridoio));
        }

        corridoi.Add(nuovoCorridoio);
    }

    public void function(string colliderName)
    {
        GameObject nuovoCorridoio = null;
        if (colliderName == "GenerateRoomDestra")
        {
            GameObject spawnerDestro = corridoi.ElementAt(corridoi.Count - 1).transform.Find("SpawnerDestro")?.gameObject;
            posizioneCorridoio = spawnerDestro.transform.position;
            rotazioneCorridoio += new Vector3(0f, 90f, 0f); // Ruota di 90° per ogni nuovo corridoio
            CreaStanza(nuovoCorridoio);
        }
        else if (colliderName == "GenerateRoomSinistra")
        {
            GameObject spawnerSinistro = corridoi.ElementAt(corridoi.Count - 1).transform.Find("SpawnerSinistro")?.gameObject;
            posizioneCorridoio = spawnerSinistro.transform.position;
            rotazioneCorridoio += new Vector3(0f, -90f, 0f); // Ruota di 90° per ogni nuovo corridoio
            CreaStanza(nuovoCorridoio);
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

    public void ChiudiPorta()
    {   
        StartCoroutine(WaitChiudiPorta());
        StartCoroutine(sottotitoli(voiceLines[livello], 1f));
        livello++;
    }

    IEnumerator WaitChiudiPorta()
    {
        Domanda = true;
        if(livello >= 1 && !SecondaMusica){
            MusicManager.instance.StopMusic();  // Fermo tutto
            MusicManager.instance.PlayMusic(Music[1], transform, 0.7f);   // Faccio partire la prossima canzone
            SecondaMusica = false;
        }
        
        GameObject door_1 = corridoi.ElementAt(corridoi.Count - 2).transform.Find("Door_1")?.gameObject;
        GameObject door1 = door_1.transform.Find("Door")?.gameObject;
        doorScript1 = door1.GetComponent<Door>();
        GameObject door_2 = corridoi.ElementAt(corridoi.Count - 2).transform.Find("Door_2")?.gameObject;
        GameObject door2 = door_2.transform.Find("Door")?.gameObject;
        doorScript2 = door2.GetComponent<Door>();
        if (doorScript2.open)
        {
            doorScript2.OpenDoor();
            yield return new WaitForSeconds(0.7f);
        }
        doorScript2.openDoor = null;
        doorScript2.closeDoor = null;
        doorScript2.enabled = false;
        if (doorScript1.open)
        {
            doorScript1.OpenDoor();
            yield return new WaitForSeconds(0.7f);
        }
        doorScript1.openDoor = null;
        doorScript1.closeDoor = null;
        doorScript1.enabled = false;
    }
}