using System.Collections;
using TMPro;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    [SerializeField] private float typingSpeed = 0.05f; // Speed of the typing effect
    private GameObject Manager;
    private GameManager gameManager;
    public GameObject Corridoio;
    private TextMeshPro Domanda; // Reference for the 3D TextMeshPro component

    private void Start()
    {
        Manager = GameObject.Find("GameManager");
        gameManager = Manager.GetComponent<GameManager>();

        Transform TransformDomanda = Corridoio.transform.Find("Domanda");
        Domanda = TransformDomanda.GetComponent<TextMeshPro>(); // Get the 3D TextMeshPro component
        if (Domanda != null)
        {
            StartCoroutine(PlayQuestion(gameManager.questions[gameManager.livello]));
            gameManager.Domanda = false;
        }
    }

    private void Update()
    {
        if (gameManager.Domanda)
        {
            if (Domanda != null)
            {
                StartCoroutine(PlayQuestion(gameManager.questions[gameManager.livello]));
                gameManager.Domanda = false;
            }
        }
    }

    // Start the typing animation with the question text
    public IEnumerator PlayQuestion(string text)
    {
        yield return StartCoroutine(TypeQuestion(text)); // Wait for the typing animation to complete
    }

    // Typing animation effect for 3D TextMeshPro
    private IEnumerator TypeQuestion(string text)
    {
        Domanda.text = ""; // Svuota il testo precedente
        foreach (char letter in text)
        {
            Domanda.text += letter; // Aggiungi una lettera per volta
            yield return new WaitForSeconds(typingSpeed); // Ritardo tra ogni lettera
        }
    }
}