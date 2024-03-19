using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SerializeField] private GameObject playerIgnis;
    [SerializeField] private GameObject playerAqua;

    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = this.transform;
        GameObject temp;

        if (IsOwner)
        {
            Instantiate(playerIgnis);
        }
        else if (IsClient)
        {
            Instantiate(playerAqua);
        }
        GameObject.Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
