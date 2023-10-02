using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Round : NetworkBehaviour
{
    [SerializeField] private int speed = 50;
    [SerializeField] private Collider main;
    [SerializeField] private Collider splash;
    [SerializeField] private Transform muzzle;
    [SerializeField] private GameObject[] Players;
    [SerializeField] private GameObject thisPlayer; 
    public bool splashdamage = false;
    private void Start()
    {

    }
    void Update()
    {
        StartCoroutine(startDamage());

        if (speed > 0)
        {
            gameObject.GetComponent<Rigidbody>().velocity = speed * gameObject.transform.up;
            StartCoroutine(stop());

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Ground")
        {
            main.enabled = false;
            splash.enabled = true;
            splashdamage = true;
            StartCoroutine(destory());
        }
        if(other.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
    IEnumerator destory()
    {
        yield return new WaitForSeconds(5);
        GameObject.DestroyObject(gameObject);
    }
    IEnumerator startDamage()
    {
        yield return new WaitForSeconds(0.3f);
        main.enabled = true;
    }
    IEnumerator stop()
    {
        yield return new WaitForSeconds(1);
        speed = 0;
    }
}
