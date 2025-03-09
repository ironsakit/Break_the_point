using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DoorScript;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject Corridoio;
    public GameObject CorridoioConnesso;
    public GameObject CorridoioConnessoLivello5;
    public GameObject CorridoioConnessoLivello7;
    public GameObject CorridoioConnessoLivello9;
    public GameObject CorridoioConnessoLivello10;
    public GameObject CorridoioConnessoLivello14;
    public GameObject CorridoioConnessoLivello15;
    public GameObject CorridoioConnessoLivello17;
    public bool jumpscare = false;


    public bool evento = true;
    private Vector3 posizioneCorridoio;
    private Vector3 rotazioneCorridoio;
    private Door doorScript2;
    private Door doorScript1;
    public int livello = 0;
    public int counter = 0;
    [SerializeField] private AudioClip[] Music;
    [SerializeField] private AudioClip[] Effect;
    public bool SecondaMusica = false;
    private bool TerzaMusica = false;
    public AudioSource musica;
    public GameObject spawnerDestro = null, spawnerSinistro = null;
    public bool rispostaGiusta = false;
    private int currentSceneIndex;
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

    private int indexError = 0;

    string[] errorVoicelines =
    {
        "Try another way",
        "You got this!",
        "You can do it",
        "You are dead",
        "You are alone",
        "You've never made a choice",
        "You are not real",
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
    "What does 'to leave' mean?",
    "How many symbols are written on the walls?",
    "Which door should you choose?",
    "Why do you keep going?",
    "Who is talking to you?",
    "How many times did the light flash?",
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
    "10",
    "Left",
    "Because I must",
    "6",
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
    "11",
    "I don’t know",
    "7",
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
    { 1, 0 }, // "Which of these is a primary color?" -> Green (sbagliato), Red (giusto)
    { 0, 1 }, // "Which sense is used to hear?" -> Taste (sbagliato), Hearing (giusto)
    { 0, 1 }, // "If you have 6 apples and give away 4, how many do you have left?" -> 2 (giusto), 6 (sbagliato)
    { 1, 0 }, // "Who wrote 'Romeo and Juliet'?" -> Shakespeare (giusto), Dante (sbagliato)
    { 1, 0 }, // "What is the most real thing?" -> Illusion (sbagliato), Your thoughts (giusto)
    { 1, 0 }, // "Where are you now?" -> Home (giusto), Nowhere (sbagliato)
    { 0, 1 }, // "Time flows...?" -> Backwards (sbagliato), Forward (giusto)
    { 0, 1 }, // "What does 'to leave' mean?" -> To stay (sbagliato), To go (giusto)
    { 1, 0 }, // "What is behind this door?" -> Nothing (giusto), A secret (sbagliato)
    { 0, 0 }, // "How many symbols are written on the walls?
    { 1, 0 }, // "Do you like this game?" -> Yes (giusto), No (sbagliato)
    { 1, 0 }, // "How many times did the light flash?
    { 1, 0 },  // "What was the initial question?" -> The first one (giusto), I don't remember (sbagliato)
    { 1, 1 }
};


    void Start()
    {
        // Inizializza la posizione del primo corridoio
        posizioneCorridoio = new Vector3(22.4427f, 0f, 21.48035f);  // Coordinate della stanza principale
        rotazioneCorridoio = new Vector3(-90f, 0f, 0f);  // Rotazione iniziale del corridoio
        GameObject gameObject = Instantiate(Corridoio, posizioneCorridoio, Quaternion.Euler(rotazioneCorridoio));
        corridoi.Add(gameObject);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // Ottiene l'indice della scena attuale
        if (currentSceneIndex == 3)
        {
            StartCoroutine(Finale());
        }
        else
        {
            musica = MusicManager.instance.PlayMusic(Music[0], transform, 0.7f);
            StartCoroutine(sottotitoli(voiceLines[0], 1f, Color.white));
        }
    }

    private IEnumerator Finale()
    {
        yield return sottotitoli("After all this time, you still don't understand.", 1f, Color.white);
        yield return sottotitoli("That you are in an eternal cycle of suffering?", 1f, Color.white);
        yield return sottotitoli("You are trapped here forever for your sins.", 1f, Color.white);
        yield return sottotitoli("Enjoy this eternity in hell.", 1f, Color.red);
        yield return sottotitoli("You never had a choice.", 1f, Color.red);
        Luci luci = corridoi.ElementAt(corridoi.Count - 1).GetComponent<Luci>();
        luci.livello7 = 1;
        AudioSource eletc = MusicManager.instance.PlayMusic(Effect[2], transform, 0.4f);
        yield return new WaitForSeconds(3f);
        eletc.Stop();
        luci.SpegniTutto();
        StopAllSounds();
        SceneManager.LoadScene(4);
    }
    public void StopAllSounds()
    {
        AudioSource[] allAudioSources = FindObjectsByType<AudioSource>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (AudioSource audioSource in allAudioSources)
        {
            audioSource.Stop(); // Ferma ogni suono attivo
        }
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

    private int livelloPrecedente; // Variabile per memorizzare il livello prima dell'aggiornamento
    private int counter2 = 0;
    public void function(string colliderName)
    {
        if (currentSceneIndex < 3)
        {
            GameObject nuovoCorridoio = null;

            if (colliderName == "GenerateRoomDestra" && ((livello != 16) || !jumpscare))
            {
                livelloPrecedente = livello; // Salva il livello attuale prima dell'aggiornamento

                spawnerDestro = corridoi.ElementAt(corridoi.Count - 1).transform.Find("SpawnerDestro")?.gameObject;
                posizioneCorridoio = spawnerDestro.transform.position;
                rotazioneCorridoio += new Vector3(0f, 90f, 0f);

                if (answerMatrix[livello, 1] == 1)
                {
                    livello++; // Incrementa il livello solo se la risposta è giusta
                    rispostaGiusta = true;
                    evento = true;
                    counter2 = counter;
                    counter = 0;
                }
                else
                {
                    evento = false;
                    rispostaGiusta = false;
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
                    rispostaGiusta = true;
                    evento = true;
                    counter2 = counter;
                    counter = 0;
                }
                else
                {
                    evento = false;
                    rispostaGiusta = false;
                }

                CreaStanza();
            }
            else if (colliderName == "DestroyRoomDestra")
            {   
                // Usa il livello precedente per controllare la risposta giusta
                if (answerMatrix[livelloPrecedente, 1] == 1)
                {   
                    if(livello != 16)
                    {
                        livello--; // Decrementa il livello se la risposta era giusta
                    }
                    rispostaGiusta = false;
                    evento = false;
                    counter = counter2;
                }

                StartCoroutine(DistruggiStanza("SpawnerDestro", 90f));
            }
            else if (colliderName == "DestroyRoomSinistra")
            {   
               
                // Usa il livello precedente per controllare la risposta giusta
                if (answerMatrix[livelloPrecedente, 0] == 1)
                {
                    livello--; // Decrementa il livello se la risposta era giusta
                    rispostaGiusta = false;
                    evento = false;
                    counter = counter2;
                }

                StartCoroutine(DistruggiStanza("SpawnerSinistro", -90f));
            }
        }
    }

    public void AvviaLivello7(float durata, bool distruggi, bool automatico)
    {
        StartCoroutine(Livello7(durata, distruggi, automatico));
    }

    public IEnumerator Livello7(float durata, bool distruggi, bool automatico)
    {
        Animator enemyAnimator;
        GameObject nuovoCorridoio = null;
        spawnerDestro = corridoi.ElementAt(corridoi.Count - 1).transform.Find("SpawnerDestro")?.gameObject;

        Luci luci = corridoi.ElementAt(corridoi.Count - 1).GetComponent<Luci>();

        luci.livello7 = 1;
        SoundFXManager.instance.PlaySoundFXClip(Effect[0], transform, 2f);
        if (spawnerDestro == null)
        {
            Debug.LogError("SpawnerDestro non trovato!");
            yield break;
        }

        posizioneCorridoio = spawnerDestro.transform.position;
        rotazioneCorridoio += new Vector3(0f, 90f, 0f);

        //prendo la porta desta e la apro
        GameObject door_1 = corridoi.ElementAt(corridoi.Count - 1).transform.Find("Door_1")?.gameObject;
        GameObject door1 = door_1.transform.Find("Door")?.gameObject;

        doorScript1 = door1.GetComponent<Door>();
        if (automatico)
        {
            doorScript1.OpenDoor();  //apro la porta
        }
        

        nuovoCorridoio = Instantiate(CorridoioConnessoLivello7, posizioneCorridoio, Quaternion.Euler(rotazioneCorridoio));

        GameObject enemy = nuovoCorridoio.transform.Find("enemy")?.gameObject;
        enemy.SetActive(true);
        AudioSource eletc = MusicManager.instance.PlayMusic(Effect[2], transform, 0.4f);
        enemyAnimator = enemy.GetComponent<Animator>();
        enemyAnimator.SetBool("yell", true);  // imposto la animazione in yell

        corridoi.Add(nuovoCorridoio);

        yield return new WaitForSeconds(durata);
        luci.livello7 = -1;
        if (automatico)
        {
            doorScript1.OpenDoor();  //apro la porta
        }
        eletc.Stop();
        enemy.SetActive(false);
        if (distruggi)
        {
            StartCoroutine(DistruggiStanza("SpawnerDestro", 90f));
        }
        
    }

    public void AvviaLivello9()
    {
        StartCoroutine(Livello9());
    }

    public IEnumerator Livello9()
    {
        Animator maskAnimator;
        GameObject mask = corridoi.ElementAt(corridoi.Count - 1).transform.Find("maschera")?.gameObject;
        maskAnimator = mask.GetComponent<Animator>();

        maskAnimator.SetBool("showMask", true);
        maskAnimator = mask.GetComponent<Animator>();
        SoundFXManager.instance.PlaySoundFXClip(Effect[1], transform, 1.5f);
        yield return new WaitForSeconds(1f);
        maskAnimator.SetBool("showMask", false);
        mask.SetActive(false);
    }

    public void AvviaLivello14()
    {
        StartCoroutine(Livello14());
    }

    public IEnumerator Livello14()
    {
        Animator maskAnimator;
        GameObject mask = corridoi.ElementAt(corridoi.Count - 1).transform.Find("maschera")?.gameObject;
        maskAnimator = mask.GetComponent<Animator>();
        maskAnimator.SetBool("showMask", true);
        maskAnimator = mask.GetComponent<Animator>();
        SoundFXManager.instance.PlaySoundFXClip(Effect[1], transform, 1.5f);
        yield return new WaitForSeconds(1f);
        mask.SetActive(false);
    }

    public void AvviaLivello17()
    {
         Luci luciscript = corridoi.ElementAt(corridoi.Count - 1).GetComponent<Luci>();
        luciscript.StartCoroutine(luciscript.cambiaIntensita3());
    }

    public void cambiaRisposte(int index, string valoreSinistra, string valoreDestra)
    {
        leftAnswers[index] = valoreSinistra;
        rightAnswers[index] = valoreDestra;
    }

    public void cambiaRispostaGiusta(int index, int valoreSinistra, int valoreDestra)
    {
        answerMatrix[index, 0] = valoreSinistra;
        answerMatrix[index, 1] = valoreDestra;
    }


    /* CREA STANZA */
    private void CreaStanza()
    {
        GameObject nuovoCorridoio = null;
        if (livello > 0 && livello < 10)
        {   
            if(livello == 5)
            {
                nuovoCorridoio = Instantiate(CorridoioConnessoLivello5, posizioneCorridoio, Quaternion.Euler(rotazioneCorridoio));
            }
            else if (livello == 7)
            {
                nuovoCorridoio = Instantiate(CorridoioConnesso, posizioneCorridoio, Quaternion.Euler(rotazioneCorridoio));
            }else if(livello == 9)
            {
                nuovoCorridoio = Instantiate(CorridoioConnessoLivello9, posizioneCorridoio, Quaternion.Euler(rotazioneCorridoio));
            }
            else
            {
                nuovoCorridoio = Instantiate(CorridoioConnesso, posizioneCorridoio, Quaternion.Euler(rotazioneCorridoio));
            }
        }
        else if (livello >= 10 && livello < 15)
        {
            if (livello == 14)
            {
                nuovoCorridoio = Instantiate(CorridoioConnessoLivello14, posizioneCorridoio, Quaternion.Euler(rotazioneCorridoio));
            }
            else
            {
                nuovoCorridoio = Instantiate(CorridoioConnessoLivello10, posizioneCorridoio, Quaternion.Euler(rotazioneCorridoio));
            }
        }
        else if (livello == 15)
        {
            nuovoCorridoio = Instantiate(CorridoioConnessoLivello15, posizioneCorridoio, Quaternion.Euler(rotazioneCorridoio));
            if (counter == 0)
            {
                GameObject Switch = nuovoCorridoio.transform.Find("switch")?.gameObject;
                Switch.SetActive(false);  //nascondo l'interruttore inizialmente
            } 
        }else if(livello == 17)
        {
            nuovoCorridoio = Instantiate(CorridoioConnessoLivello17, posizioneCorridoio, Quaternion.Euler(rotazioneCorridoio));
        }else if(livello == 18 || livello == 19)
        {
            nuovoCorridoio = Instantiate(CorridoioConnessoLivello10, posizioneCorridoio, Quaternion.Euler(rotazioneCorridoio));
            Luci luci = nuovoCorridoio.GetComponent<Luci>();
            luci.livello7 = 1;
        }
        else
        {
            nuovoCorridoio = Instantiate(CorridoioConnessoLivello10, posizioneCorridoio, Quaternion.Euler(rotazioneCorridoio));
        }

        corridoi.Add(nuovoCorridoio);
    }

    /* DISTRUGGI STANZA */
    IEnumerator DistruggiStanza(string Spawner, float rotation)
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(corridoi[corridoi.Count - 1]);  // Distruggi il corridoio più vecchio
        corridoi.RemoveAt(corridoi.Count - 1);  // Rimuovilo dalla lista
        GameObject spawner = corridoi.ElementAt(corridoi.Count - 1).transform.Find(Spawner)?.gameObject;
        posizioneCorridoio = spawner.transform.position;
        rotazioneCorridoio -= new Vector3(0f, rotation, 0f); // Ruota di 90° per ogni nuovo corridoio
    }

    IEnumerator sottotitoli(string text, float waitSeconds, Color color)
    {
        yield return new WaitForSeconds(waitSeconds);
        yield return SubtitleManager.instance.PlaySubtitle(text, 1f, color);
    }

    public void ChiudiPorta()
    {   
        StartCoroutine(WaitChiudiPorta());
        if (rispostaGiusta && livello < voiceLines.Length)
        {   
            if(livello == 16)
            {
                jumpscare = true;
            }
            StartCoroutine(sottotitoli(voiceLines[livello], 1f, Color.white));
            indexError = 0;
            counter = 0;
        }
        else
        {   
            if(counter > 0 && livello == 15)  // suggerimenti
            {
                StartCoroutine(sottotitoli("Try to find a switch", 1f, Color.red));  
            }
            else
            {
                StartCoroutine(sottotitoli(errorVoicelines[indexError], 1f, (indexError > 2) ? Color.red : Color.white));
                if (indexError < 6)
                {
                    indexError++;
                }
                counter++;   // tengo conto di quante volte ha sbagliato il giocatore
                rispostaGiusta = false;
            }
            
        }
    }
    IEnumerator WaitChiudiPorta()
    {
        if (livello >= 10 && !SecondaMusica)
        {
            StartCoroutine(FadeOutAndChangeMusic(Music[1]));
            SecondaMusica = true;
        }else if (livello >= 18 && !TerzaMusica)
        {
            StartCoroutine(FadeOutAndChangeMusic(Music[3]));
            TerzaMusica = true;
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