using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    public float playerSpeed = 40f;

    public CharacterController2D controller;

    public Animator animator;

    private float horizontalMove = 0f;

    private bool jump = false;

    private bool crouch = false;

    private GameObject player;

    [SerializeField] private CinemachineVirtualCamera cam;

    private NetworkVariable<CustomData> randomNumber = new NetworkVariable<CustomData>(new CustomData
    {
        _int = 69,
        _bool = true,
        _msg = "sth",
    },
        NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public struct CustomData : INetworkSerializable
    {
        public int _int;
        public bool _bool;
        public FixedString128Bytes _msg;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref _int);
            serializer.SerializeValue(ref _bool);
            serializer.SerializeValue(ref _msg);
        }
    }

    public override void OnNetworkSpawn()
    {
        randomNumber.OnValueChanged += (CustomData previousValue, CustomData newValue) =>
        {
            Debug.Log(OwnerClientId + "; randomNumber: " + newValue._int + "; " + newValue._bool
                + "; " + newValue._msg);
        };
    }

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject;
        player.transform.position = GameObject.FindGameObjectWithTag("Spawnpoint").GetComponent<Transform>().position;
        if (IsLocalPlayer)
        {
            cam = CinemachineVirtualCamera.FindObjectOfType<CinemachineVirtualCamera>();
            cam.m_Follow = player.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            TestServerRpc(new ServerRpcParams());
            /*randomNumber.Value = new CustomData
            {
                _int = 10,
                _bool = false,
                _msg = "test msg",
            };*/
        }

            if (string.Equals(player.name, "Player1", StringComparison.OrdinalIgnoreCase))
        {
            horizontalMove = Input.GetAxisRaw("Player1_Horizontal") * playerSpeed;
            if (Input.GetButtonDown("Player1_Jump"))
            {
                jump = true;
                animator.SetBool("IsJumping", true);
            }
            if (Input.GetButtonDown("Player1_Crouch"))
            {
                crouch = true;
            }
            else if (Input.GetButtonUp("Player1_Crouch"))
            {
                crouch = false;
            }
        }
        if (string.Equals(player.name, "Player2", StringComparison.OrdinalIgnoreCase))
        {
            horizontalMove = Input.GetAxisRaw("Player2_Horizontal") * playerSpeed;
            if (Input.GetButtonDown("Player2_Jump"))
            {
                jump = true;
                animator.SetBool("IsJumping", true);
            }
            if (Input.GetButtonDown("Player2_Crouch"))
            {
                crouch = true;
            }
            else if (Input.GetButtonUp("Player2_Crouch"))
            {
                crouch = false;
            }
        }

        horizontalMove = Input.GetAxisRaw("Player1_Horizontal") * playerSpeed;
        if (Input.GetButtonDown("Player1_Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }
        if (Input.GetButtonDown("Player1_Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Player1_Crouch"))
        {
            crouch = false;
        }
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    [ServerRpc]
    private void TestServerRpc(ServerRpcParams serverRpcParams)
    {
        Debug.Log("TestServerRpc: " + OwnerClientId + "; Message: " + serverRpcParams.Receive.SenderClientId);
    }

    [ClientRpc]
    private void TestClientRpc()
    {

    }
}
