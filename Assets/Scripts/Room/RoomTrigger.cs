using System.Collections;
using DoorScript;
using TMPro;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    public GameObject Corridoio;
    private GameObject Manager;
    private GameManager gameManager;
    public GameObject collider;
    private GameObject door_2;
    private Door doorScript2;
    private GameObject door_1;
    private Door doorScript1;
    private TextMeshPro Domanda; // Reference for the 3D TextMeshPro component
    private QuestionManager questionManager;
    private TextMeshPro RispostaSinistra; // Reference for the 3D TextMeshPro component
    private QuestionManager questionManagerSinistra;
    private TextMeshPro RispostaDestra; // Reference for the 3D TextMeshPro component
    private QuestionManager questionManagerDestra;

    void Start()
    {
        Manager = GameObject.Find("GameManager");
        gameManager = Manager?.GetComponent<GameManager>();
        door_2 = Corridoio.transform.Find("Door_2")?.gameObject;
        GameObject door = door_2.transform.Find("Door")?.gameObject;
        doorScript2 = door.GetComponent<Door>();
        door_1 = Corridoio.transform.Find("Door_1")?.gameObject;
        GameObject door1 = door_1.transform.Find("Door")?.gameObject;
        doorScript1 = door1.GetComponent<Door>();

        Transform TransformDomanda = Corridoio.transform.Find("Domanda");
        Domanda = TransformDomanda.GetComponent<TextMeshPro>(); // Get the 3D TextMeshPro component
        questionManager = Domanda.GetComponent<QuestionManager>();

        Transform TransformRispostaSinistra = Corridoio.transform.Find("RispostaSinistra");
        RispostaSinistra = TransformRispostaSinistra.GetComponent<TextMeshPro>(); // Get the 3D TextMeshPro component
        questionManagerSinistra = RispostaSinistra.GetComponent<QuestionManager>();

        Transform TransformRispostaDestra = Corridoio.transform.Find("RispostaDestra");
        RispostaDestra = TransformRispostaDestra.GetComponent<TextMeshPro>(); // Get the 3D TextMeshPro component
        questionManagerDestra = RispostaDestra.GetComponent<QuestionManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            if (gameObject.name == "DestroyRoomSinistra")
            {
                if (doorScript2 != null && doorScript2.open)
                {
                    doorScript2.OpenDoor();
                }
            }
            else if (gameObject.name == "DestroyRoomDestra")
            {
                if (doorScript1 != null && doorScript1.open)
                {
                    doorScript1.OpenDoor();
                }
            }
            else if (gameObject.name == "ChiudiPorta")
            {
                if (gameManager.livello == 17)
                {
                    gameManager.AvviaLivello17();
                }
                gameManager.ChiudiPorta();
                gameObject.SetActive(false);
            }
            else if (gameObject.name == "GeneraDomanda")
            {
                if (questionManager != null && gameManager != null)
                {
                    StartCoroutine(questionManager.PlayQuestion(gameManager.questions[gameManager.livello]));
                    StartCoroutine(questionManagerSinistra.PlayQuestion(gameManager.leftAnswers[gameManager.livello]));
                    StartCoroutine(questionManagerDestra.PlayQuestion(gameManager.rightAnswers[gameManager.livello]));
                }
                if (gameManager.livello == 7 && gameManager.evento)
                {
                    gameManager.AvviaLivello7(2f, true, true);
                    gameManager.evento = false;
                }
                else if (gameManager.livello == 9 && gameManager.evento)
                {
                    gameManager.AvviaLivello9();
                    gameManager.evento = false;
                }
                else if (gameManager.livello == 14 && gameManager.evento)
                {
                    gameManager.AvviaLivello14();
                    gameManager.evento = false;
                }
                gameObject.SetActive(false);
            }

            if (gameObject.name != "GeneraDomanda")
            {
                if (collider != null)
                {
                    collider.SetActive(true);
                }
                if (gameManager != null)
                {
                    gameManager.function(gameObject.name);
                }
                gameObject.SetActive(false);
            }
        }
    }
}
