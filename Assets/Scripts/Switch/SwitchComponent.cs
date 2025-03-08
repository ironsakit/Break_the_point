using JetBrains.Annotations;
using TMPro;
using UnityEngine;
namespace SwitchScript
{
    [RequireComponent(typeof(AudioSource))]
    public class SwitchComponent : MonoBehaviour
    {
        public Light[] SpotLights;
        public TextMeshPro RispostaS, RispostaD;
        private GameManager gameManager;
        public GameObject[] sangue;
        public bool on = false;
        public AudioSource asource;
        public AudioClip switchOn, switchOff;
        void Start()
        {
            asource = GetComponent<AudioSource>();
            GameObject GameManager = GameObject.Find("GameManager");
            gameManager = GameManager.GetComponent<GameManager>();
            
        }

        public void TurnOnOff()
        {
            if (!on)
            {
                RispostaD.text = "16";
                RispostaS.text = "14";
                gameManager.cambiaRisposte(15, "14", "16");  // modifico le risposte
                gameManager.cambiaRispostaGiusta(15, 0, 1);  // modifico quale è quella giusta
                on = !on;
                asource.clip = on ? switchOn : switchOff;
                asource.Play();
                for (int i = 0; i < SpotLights.Length; i++)
                {
                    if (!on)
                    {
                        SpotLights[i].intensity = 0f;
                    }
                    else
                    {
                        SpotLights[i].intensity = 133.95f;
                    }

                }
                for (int i = 0; i < sangue.Length; i++)
                {
                    if (on)
                    {
                        sangue[i].SetActive(false);
                    }
                    else
                    {
                        sangue[i].SetActive(true);
                    }

                }
            }
            
        }
    }
}