using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class RoundShoot : NetworkBehaviour
{
    [SerializeField] private GameObject roundPrefab;
    [SerializeField] private GameObject[] Players;
    [SerializeField] private GameObject thisPlayer;
    [SerializeField] private Transform muzzle;
    [SerializeField] private float Cooldown = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Players = GameObject.FindGameObjectsWithTag("Player");
        if(IsOwner)
        {
            thisPlayer = Array.FindAll(Players, x => x.GetComponent<Movement>().IsOwner)[0];
        }
        muzzle = thisPlayer.transform.GetChild(4).GetChild(0);

    }

    // Update is called once per frame
    void Update()
    {
        Cooldown -= Time.deltaTime;
        if (IsOwner)
        {
            if (Input.GetMouseButton(0) && Cooldown <= 0)
            {
                ShootServerRpc();
                Cooldown = 10;
            }
        }


    }
    [ServerRpc]
    private void ShootServerRpc()
    {
        GameObject roundPrefabnetwork = Instantiate(roundPrefab, muzzle.position,roundPrefab.transform.rotation);
        roundPrefabnetwork.GetComponent<NetworkObject>().Spawn();
    }
}
