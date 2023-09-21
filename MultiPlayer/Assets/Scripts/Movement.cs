using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Cinemachine;

public class Movement : NetworkBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float sprintSpeed = 16f;

    Vector3 velocity;

    public float gravity = -9.81f;

    public float JumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundLayerMask;

    private bool isGrounded;

    public bool sprint;

    public float energy = 0f;

    public CinemachineVirtualCamera vc;

    public AudioListener listener;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (IsLocalPlayer)
        {
            Move();
            Ground();
            Sprint();
        }
    }
    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        if (sprint == true && energy <= 10)
        {
            energy += Time.deltaTime;
            controller.Move(move * sprintSpeed * Time.deltaTime);

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }
        else if (energy <= 0)
        {
            energy = 0;
            controller.Move(move * speed * Time.deltaTime);

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }
        else if (energy >= 0 && sprint == false)
        {
            energy -= 0.1f;
            controller.Move(move * speed * Time.deltaTime);

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }
    }
    private void Ground()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayerMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(JumpHeight * -2f * gravity);
        }

    }
    private void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            sprint = true;
        }
        else
        {
            sprint = false;
        }
    }
    public override void OnNetworkSpawn()
    {
        transform.position = new Vector3(Random.RandomRange(5f, -5f), 1, Random.RandomRange(5f, -5f));
        if (IsOwner)
        {
            listener.enabled = true;
            vc.Priority = 1;
        }
        else
        {
            vc.Priority = 0;
        }
    }
}
