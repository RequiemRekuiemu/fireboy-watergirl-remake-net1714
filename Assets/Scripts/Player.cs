using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SerializeField] private GameObject playerIgnis;
    [SerializeField] private GameObject playerAqua;

    private Transform spawnPointIgnis;
    private Transform spawnPointAqua;

    // Start is called before the first frame update
    void Start()
    {
        spawnPointIgnis = GameObject.FindGameObjectWithTag("SpawnPointIgnis").GetComponent<Transform>();
        spawnPointAqua = GameObject.FindGameObjectWithTag("SpawnPointAqua").GetComponent<Transform>();

        if (IsOwner)
        {
            Instantiate(playerIgnis, spawnPointIgnis);
        }
        else if (IsClient)
        {
            Instantiate(playerAqua, spawnPointAqua);
        }
        GameObject.Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
