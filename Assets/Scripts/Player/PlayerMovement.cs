using System;
using System.Collections;
using Unity.Mathematics.Geometry;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public float DistanceOpen = 3f;
    // Sensibilità del mouse
    public float mouseSensitivity = 300f;
    // Input del mouse
    private float xRotation = 0f;
    public Animator playerAnimator;
    [SerializeField] private AudioClip[] stepSounds;
    private float tempo = 0;
    private int Index = 0;

    // Riferimento alla Camera
    public Transform playerCamera;

    // Velocità massima del giocatore
    public float maxSpeed;
    private float speedSound;
    // Riferimento a PlayerAnimation
    public bool canMove = false;
    private CharacterController characterController;
    public bool doorOpen = false;

    void Start()
    {
        // Blocca il cursore al centro dello schermo
        Cursor.lockState = CursorLockMode.Locked;
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        OpendDoor();
        TurnSwitch();
        // Gestisci il movimento del mouse
        MouseLook();
        // Gestisci il movimento del giocatore
        MovePlayer();
    }

    void OpendDoor()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, DistanceOpen))
        {
            if (hit.transform.GetComponent<DoorScript.Door>())
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.transform.GetComponent<DoorScript.Door>().OpenDoor();
                    doorOpen = true;
                }
            }
        }
    }

    void TurnSwitch()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, DistanceOpen))
        {
            if (hit.transform.GetComponent<SwitchScript.SwitchComponent>())
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    hit.transform.GetComponent<SwitchScript.SwitchComponent>().TurnOnOff();
                }
            }
        }
    }

    void MouseLook()
    {
        // Legge l'input del mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotazione verticale della camera
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 73f); // Limita l'angolo verticale

        // Applica la rotazione verticale alla camera
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Applica la rotazione orizzontale al personaggio
        transform.Rotate(Vector3.up * mouseX);
    }

    void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = transform.right * horizontal + transform.forward * vertical;
        Vector3 movement = direction.normalized * maxSpeed * Time.deltaTime;

        tempo += Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerAnimator.speed = 1.4f;
            speedSound = 0.15f;
            maxSpeed = 15f;
        }
        else
        {
            playerAnimator.speed = 1f;
            speedSound = 0.25f;
            maxSpeed = 10f;
        }

        // Muovi il personaggio tramite il CharacterController
        characterController.Move(movement);

        // Controlla se il personaggio si sta muovendo effettivamente
        if (tempo > (stepSounds[Index].length + speedSound) && (horizontal != 0 || vertical != 0))
        {
            // Usa la velocità effettiva del CharacterController per verificare il movimento
            if (characterController.velocity.magnitude > 0.1f) // 0.1 è una soglia per evitare micro-movimenti
            {
                Index++;
                if (Index == stepSounds.Length)
                {
                    Index = 0;
                }
                AudioClip stepsound = stepSounds[Index];
                SoundFXManager.instance.PlaySoundFXClip(stepsound, transform, 0.5f);
                tempo = 0;
            }
        }

        playerAnimator.SetFloat("Horizontal", horizontal);
        playerAnimator.SetFloat("Vertical", vertical);
        playerAnimator.SetFloat("Speed", characterController.velocity.magnitude);
    }


}
