using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round : MonoBehaviour
{
    [SerializeField] private int speed = 5;
    [SerializeField] private GameObject muzzle;
    [SerializeField] private Collider main;
    [SerializeField] private Collider splash;
    private void Start()
    {
        muzzle = GameObject.FindGameObjectWithTag("Muzzle");
        gameObject.transform.position = muzzle.transform.position;
    }
    void Update()
    {
        if (speed > 0)
        {
            gameObject.GetComponent<Rigidbody>().velocity = transform.forward * speed;
            StartCoroutine(stop());

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.tag == "Ground")
        {
            main.enabled = false;
            splash.enabled = true;
            StartCoroutine(destory());
        }
    }
    IEnumerator destory()
    {
        yield return new WaitForSeconds(5);
        GameObject.DestroyObject(gameObject);
    }
    IEnumerator stop()
    {
        yield return new WaitForSeconds(1);
        speed = 0;
    }
}
