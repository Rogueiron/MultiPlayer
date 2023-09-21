using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Health : NetworkBehaviour
{
    public NetworkVariable<int> health = new NetworkVariable<int>();
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        health.Value = 100;
    }
}
