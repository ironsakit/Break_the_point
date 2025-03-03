using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubtitleManager : MonoBehaviour
{
    public static SubtitleManager instance;
    [SerializeField] private GameObject subtitlePrefab; // Prefab che contiene il Panel
    private TextMeshProUGUI subtitleText;
    public Transform Canvas;
    private RectTransform panelRectTransform;
    [SerializeField] private float typingSpeed = 0.05f;

    private GameObject currentSubtitlePanel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public IEnumerator PlaySubtitle(string text, float duration)
    {
        yield return StartCoroutine(TypeSubtitle(text, duration)); // Aspetta che il sottotitolo finisca
    }

    private IEnumerator TypeSubtitle(string text, float duration)
    {
        // Instanzia il prefab come figlio del Canvas
        currentSubtitlePanel = Instantiate(subtitlePrefab, Canvas.transform);
        subtitleText = currentSubtitlePanel.GetComponentInChildren<TextMeshProUGUI>();
        panelRectTransform = currentSubtitlePanel.GetComponent<RectTransform>();
        // Assicurati che il panel sia disattivato all'inizio
        currentSubtitlePanel.SetActive(false);

        currentSubtitlePanel.SetActive(true); // Mostra il pannello
        subtitleText.text = ""; // Svuota il testo

        float panelWidthIncrement = 45f; // Incrementa la larghezza

        AudioClip clip;

        foreach (char letter in text)
        {
            subtitleText.text += letter;
            char lettera2 = char.ToLower(letter);
            if(lettera2 >= 'a' && lettera2 <= 'z')
            {
                clip = Resources.Load<AudioClip>("Musica/alfabeto/" + lettera2);
                SoundFXManager.instance.PlaySoundFXClip(clip, transform, 1f);
            }
            // Aumenta la larghezza del pannello (questo fa sì che il pannello si allarghi)
            panelRectTransform.sizeDelta = new Vector2(panelRectTransform.sizeDelta.x + panelWidthIncrement, 100);
            yield return new WaitForSeconds(typingSpeed); // Ritardo tra ogni lettera
        }

        yield return new WaitForSeconds(duration); // Aspetta la durata
        currentSubtitlePanel.SetActive(false); // Nascondi il pannello
        Destroy(currentSubtitlePanel.gameObject);
    }
}