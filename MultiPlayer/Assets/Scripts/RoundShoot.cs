using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class RoundShoot : NetworkBehaviour
{
    [SerializeField] private GameObject roundPrefab;
    [SerializeField] private Transform muzzle;
    [SerializeField] private float Cooldown = 1f;
    // Start is called before the first frame update
    void Start()
    {

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
        GameObject roundPrefabnetwork = Instantiate(roundPrefab, muzzle.position, Quaternion.identity);
        roundPrefabnetwork.GetComponent<NetworkObject>().Spawn();
    }
}
