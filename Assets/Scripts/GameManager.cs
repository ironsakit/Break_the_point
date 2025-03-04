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
    public GameObject CorridoioConnessoLivello15;

    private Vector3 posizioneCorridoio;
    private Vector3 rotazioneCorridoio;
    private Door doorScript2;
    private Door doorScript1;
    public int livello = 0;
    [SerializeField] private AudioClip[] Music;
    public int PrimaParte = 0, SecondaParte = 1, TerzaParte = 19, UltimaParte = 20;
    private bool SecondaMusica = false;
    public AudioSource musica;
    public GameObject spawnerDestro = null, spawnerSinistro = null;
    public bool rispostaGiusta = true;

    private List<GameObject> corridoi = new List<GameObject>();

    string[] voiceLines = {
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

    [SerializeField] public string[] questions = {
    "",
    "What is the next number?",
    "Which animal CANNOT fly?",
    "How many months have 28 days?",
    "The sun is a...?",
    "What is 2+2?",
    "Which chemical element is essential for breathing?",
    "Which of these is a primary color?",
    "Which sense is used to hear?",
    "If you have 6 apples and give away 4, how many do you have left?",
    "Who wrote 'Romeo and Juliet'?",
    "What is the most real thing?",
    "Where are you now?",
    "Time flows...?",
    "What does 'to leave' mean?",
    "What is behind this door?",
    "Which door should you choose?",
    "Why do you keep going?",
    "Who is talking to you?",
    "What happens if you stop playing?",
    "What was the initial question?"
};

    [SerializeField]
    public string[] leftAnswers = {
    "",
    "2",
    "Penguin",
    "1",
    "Planet",
    "3",
    "Carbon",
    "Green",
    "Taste",
    "2",
    "Shakespeare",
    "Your thoughts",
    "Home",
    "Backwards",
    "To stay",
    "Nothing",
    "Left",
    "Because I must",
    "No one",
    "Everything stops",
    "I don't remember"
};

    [SerializeField]
    public string[] rightAnswers = {
    "",
    "5",
    "Bat",
    "12",
    "Star",
    "4",
    "Oxygen",
    "Red",
    "Hearing",
    "6",
    "Dante",
    "Illusion",
    "Nowhere",
    "Forward",
    "To go",
    "A secret",
    "Right",
    "I don’t know",
    "Someone",
    "Nothing changes",
    "The first one"
};

    [SerializeField]
    public int[,] answerMatrix = {
    { 0, 1 }, // "What is the next number?" -> 2 (sbagliato), 5 (giusto)
    { 1, 0 }, // "Which animal CANNOT fly?" -> Penguin (giusto), Bat (sbagliato)
    { 1, 0 }, // "How many months have 28 days?" -> 12 (giusto), 1 (sbagliato)
    { 0, 1 }, // "The sun is a...?" -> Planet (sbagliato), Star (giusto)
    { 0, 1 }, // "What is 2+2?" -> 3 (sbagliato), 4 (giusto)
    { 0, 1 }, // "Which chemical element is essential for breathing?" -> Carbon (sbagliato), Oxygen (giusto)
    { 0, 1 }, // "Which of these is a primary color?" -> Green (sbagliato), Red (giusto)
    { 0, 1 }, // "Which sense is used to hear?" -> Taste (sbagliato), Hearing (giusto)
    { 1, 0 }, // "If you have 6 apples and give away 4, how many do you have left?" -> 2 (giusto), 6 (sbagliato)
    { 1, 0 }, // "Who wrote 'Romeo and Juliet'?" -> Shakespeare (giusto), Dante (sbagliato)
    { 0, 1 }, // "What is the most real thing?" -> Illusion (sbagliato), Your thoughts (giusto)
    { 1, 0 }, // "Where are you now?" -> Home (giusto), Nowhere (sbagliato)
    { 0, 1 }, // "Time flows...?" -> Backwards (sbagliato), Forward (giusto)
    { 0, 1 }, // "What does 'to leave' mean?" -> To stay (sbagliato), To go (giusto)
    { 1, 0 }, // "What is behind this door?" -> Nothing (giusto), A secret (sbagliato)
    { 0, 1 }, // "Which door should you choose?" -> Left (sbagliato), Right (giusto)
    { 1, 0 }, // "Why do you keep going?" -> Because I must (giusto), I don’t know (sbagliato)
    { 1, 0 }, // "Who is talking to you?" -> Someone (giusto), No one (sbagliato)
    { 1, 0 }, // "What happens if you stop playing?" -> Everything stops (giusto), Nothing changes (sbagliato)
    { 1, 0 }  // "What was the initial question?" -> The first one (giusto), I don't remember (sbagliato)
};


    void Start()
    {
        // Inizializza la posizione del primo corridoio
        posizioneCorridoio = new Vector3(22.4427f, 0f, 21.48035f);  // Coordinate della stanza principale
        rotazioneCorridoio = new Vector3(-90f, 0f, 0f);  // Rotazione iniziale del corridoio
        GameObject gameObject = Instantiate(Corridoio, posizioneCorridoio, Quaternion.Euler(rotazioneCorridoio));
        corridoi.Add(gameObject);
        musica = MusicManager.instance.PlayMusic(Music[0], transform, 0.7f);
    }

    // Update is called once per frame
    void Update()
    {   
        if(livello == 0 && rispostaGiusta)
        {   
            StartCoroutine(sottotitoli(voiceLines[livello], 1f));
            rispostaGiusta = false;
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

    private void CreaStanza()
    {
        GameObject nuovoCorridoio = null;
        if (livello <= 1)
        {
            nuovoCorridoio = Instantiate(CorridoioConnesso, posizioneCorridoio, Quaternion.Euler(rotazioneCorridoio));
        }
        else if (livello >= 2 && livello < 4)
        {
            nuovoCorridoio = Instantiate(CorridoioConnessoLivello10, posizioneCorridoio, Quaternion.Euler(rotazioneCorridoio));
        }else if(livello >= 4)
        {   
            nuovoCorridoio = Instantiate(CorridoioConnessoLivello15, posizioneCorridoio, Quaternion.Euler(rotazioneCorridoio));
        }

        corridoi.Add(nuovoCorridoio);
    }

    private int livelloPrecedente; // Variabile per memorizzare il livello prima dell'aggiornamento

    public void function(string colliderName)
    {
        GameObject nuovoCorridoio = null;

        if (colliderName == "GenerateRoomDestra")
        {
            livelloPrecedente = livello; // Salva il livello attuale prima dell'aggiornamento

            spawnerDestro = corridoi.ElementAt(corridoi.Count - 1).transform.Find("SpawnerDestro")?.gameObject;
            posizioneCorridoio = spawnerDestro.transform.position;
            rotazioneCorridoio += new Vector3(0f, 90f, 0f);

            if (answerMatrix[livello, 1] == 1)
            {
                livello++; // Incrementa il livello solo se la risposta è giusta
            }

            CreaStanza();
        }
        else if (colliderName == "GenerateRoomSinistra")
        {
            livelloPrecedente = livello; // Salva il livello attuale prima dell'aggiornamento

            spawnerSinistro = corridoi.ElementAt(corridoi.Count - 1).transform.Find("SpawnerSinistro")?.gameObject;
            posizioneCorridoio = spawnerSinistro.transform.position;
            rotazioneCorridoio += new Vector3(0f, -90f, 0f);

            if (answerMatrix[livello, 0] == 1)
            {
                livello++; // Incrementa il livello solo se la risposta è giusta
            }

            CreaStanza();
        }
        else if (colliderName == "DestroyRoomDestra")
        {
            // Usa il livello precedente per controllare la risposta giusta
            if (answerMatrix[livelloPrecedente, 1] == 1)
            {
                livello--; // Decrementa il livello se la risposta era giusta
            }

            StartCoroutine(Wait());
        }
        else if (colliderName == "DestroyRoomSinistra")
        {
            // Usa il livello precedente per controllare la risposta giusta
            if (answerMatrix[livelloPrecedente, 0] == 1)
            {
                livello--; // Decrementa il livello se la risposta era giusta
            }

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
    }
    IEnumerator WaitChiudiPorta()
    {
        if (livello >= 2 && !SecondaMusica)
        {
            StartCoroutine(FadeOutAndChangeMusic(Music[1]));
            SecondaMusica = true;
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
    IEnumerator FadeOutAndChangeMusic(AudioClip nuovaMusica)
    {
        float volume = musica.volume;
        for (float t = 0; t < 1f; t += Time.deltaTime / 1f)
        {
            musica.volume = Mathf.Lerp(volume, 0, t);
            yield return null;
        }

        musica.Stop();
        musica.clip = nuovaMusica;
        musica.Play();

        for (float t = 0; t < 1f; t += Time.deltaTime / 1f)
        {
            musica.volume = Mathf.Lerp(0, volume, t);
            yield return null;
        }
    }
}