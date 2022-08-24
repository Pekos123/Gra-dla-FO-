using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    // no to ten towrze objekt contorller ktory bedzie moim character kontorler
    public CharacterController controller;
    [SerializeField]
    private Rigidbody rb;
    // ten vector bedzie odpowiedzialny za ruch ( min. za skok i spadanie cnie)
    private Vector3 playerVelocity;
    // bool który sprawdza czy postać jest na ziemi
    private bool groundedPlayer;
    public Transform groundCheck;
    public LayerMask gorundMask;
    public float groundDistance = 0.4f;
    // o reszcie nazwa mówi
    public float speed = 2.0f;
    public float jumpHeight = 1.0f;
    private float gravity = -9.81f;

    public int MaxDoubleJump = 2;
    private int jump;


    Dash dash;


    bool CanJump()
    {
        if(jump < MaxDoubleJump && !dash.isPlayerDashing)
            return true;
        else if(groundedPlayer && !dash.isPlayerDashing)
            return true;
        return false;
    }

    void Movment()
    {
        // tutuaj sprawdzam czy jest na ziemi
        groundedPlayer = Physics.CheckSphere(groundCheck.position, groundDistance, gorundMask);

        if (groundedPlayer && playerVelocity.y < 0)
        {
            // jęzli jest na ziemi to żeby nie spadał
            playerVelocity.y = 0f;
        }

        Vector3 move = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");

        //porusza się po ziemi

        
        if (Input.GetKeyDown(KeyCode.LeftShift) && !groundedPlayer)
        {
            StartCoroutine(dash.DashCorutine());
        }
        else
        {
            controller.Move(move * Time.deltaTime * speed);
        }

        if (Input.GetButtonDown("Jump") && CanJump())
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
            jump += 1;
        }

        if (jump >= MaxDoubleJump && groundedPlayer)
            jump = 0;

        
        playerVelocity.y += gravity * Time.deltaTime;

        //porusza się w powietrzu
        controller.Move(playerVelocity * Time.deltaTime);

        
    }

    void Start()
    {
        dash = transform.GetComponent<Dash>();
    }
    void Update()
    {
        Movment();
    }
}