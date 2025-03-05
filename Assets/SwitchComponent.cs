using UnityEngine;
namespace SwitchScript
{
    [RequireComponent(typeof(AudioSource))]
    public class SwitchComponent : MonoBehaviour
    {
        public Light[] SpotLights;
        public GameObject[] sangue;
        public bool on = false;
        public AudioSource asource;
        public AudioClip switchOn, switchOff;
        void Start()
        {
            asource = GetComponent<AudioSource>();
        }

        public void TurnOnOff()
        {
            on = !on;
            asource.clip = on ? switchOn : switchOff;
            asource.Play();
            for (int i = 0; i < SpotLights.Length; i++)
            {
                if (on)
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