using System.Collections;
using TMPro;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    public TextMeshPro Domanda; // Reference for the 3D TextMeshPro component

    // Start the typing animation with the question text
    public IEnumerator PlayQuestion(string text)
    {
        StopAllCoroutines(); // Ferma eventuali coroutine in esecuzione per evitare duplicazioni
        Domanda.text = text;
        yield return StartCoroutine(FadeInText(Domanda, 1f)); // Avvia la nuova animazione
    }

    IEnumerator FadeInText(TextMeshPro text, float duration)
    {
        Color color = text.color;
        color.a = 0; // Inizia completamente trasparente
        text.color = color;

        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            color.a = Mathf.Lerp(0, 1, time / duration); // Graduale aumento dell'alpha
            text.color = color;
            yield return null;
        }

        color.a = 1; // Assicurati che alla fine sia completamente visibile
        text.color = color;
    }
}