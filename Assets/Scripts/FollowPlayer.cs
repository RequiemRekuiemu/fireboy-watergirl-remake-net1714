using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class FollowPlayer : NetworkBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vCam;
    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            if (IsLocalPlayer)
            {
                player = GameObject.FindGameObjectWithTag("Ignis");
                if (player != null)
                {
                    vCam.m_Follow = player.transform;
                }
            }
            else
            {
                player = GameObject.FindGameObjectWithTag("Aqua");
                if (player != null)
                {
                    vCam.m_Follow = player.transform;
                }
            }    
        }
    }
}
