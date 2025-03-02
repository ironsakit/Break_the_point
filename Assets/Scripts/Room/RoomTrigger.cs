using DoorScript;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    private GameObject Manager;
    private GameManager gameManager;
    public GameObject collider;
    private GameObject door_2;
    private Door doorScript2;
    private GameObject door_1;
    private Door doorScript1;

    void Start()
    {
        Manager = GameObject.Find("GameManager");
        gameManager = Manager?.GetComponent<GameManager>();
        door_2 = GameObject.Find("Door_2");
        GameObject door = door_2.transform.Find("Door")?.gameObject;
        doorScript2 = door.GetComponent<Door>();
        door_1 = GameObject.Find("Door_1");
        GameObject door1 = door_1.transform.Find("Door")?.gameObject;
        doorScript1 = door1.GetComponent<Door>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player") // Verifica che il trigger non sia già stato attivato
        {   
            if(gameObject.name == "DestroyRoomSinistra")
            {
                if (doorScript2.open)
                {
                    doorScript2.OpenDoor();
                }
            }else if (gameObject.name == "DestroyRoomDestra")
            {
                if (doorScript1.open)
                {
                    doorScript1.OpenDoor();
                }
            }
            collider.SetActive(true);
            gameManager.function(gameObject.name); // Chiamata alla funzione per generare il corridoio
            gameObject.SetActive(false);
        }
    }
}
