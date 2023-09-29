using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UIElements;

public class Health : NetworkBehaviour
{
    public NetworkVariable<int> health = new NetworkVariable<int>(1,NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Owner);
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        health.Value = 100;

        Transform spawnPoint = GetComponent<Transform>();
    }
    public void Update()
    {
        if(health.Value <= 0)
        {
            StartCoroutine(Respawn());
        }
    }
    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(3f);

        Transform spawnPoint = GetComponent<Transform>();
        transform.position = spawnPoint.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Round")
        {
            if(other.GetComponent<Round>().splashdamage == true)
            {
                health.Value -= 50;
            }
            else
            {
                health.Value -= 100;
            }
            
        }
    }
}
