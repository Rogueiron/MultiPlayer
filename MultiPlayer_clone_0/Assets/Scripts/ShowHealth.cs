using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class ShowHealth : NetworkBehaviour
{
    public Slider healthSlider;
    public GameObject Canvas;

    void Update()
    {
        healthSlider.value = transform.GetComponentInParent<Health>().health.Value;
    }
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if(!IsOwner)
        {
            return;
        }
            Canvas.SetActive(true);
    }
}
