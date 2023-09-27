using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Cinemachine;

public class Shoot : NetworkBehaviour
{
    public Transform Trail;
    public float range = 50f;
    [SerializeField] private float duration = 0.1f;
    public float fireRate = 1f;
    [SerializeField] Camera cam;

    LineRenderer line;

    float fireTimer;

    private bool sprint;
    private Ray ray;

    [SerializeField] private int damage = 25;
    // Start is called before the first frame update
    void Awake()
    {
        line = GetComponent<LineRenderer>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (IsOwner)
        {
            fireTimer += Time.deltaTime;
            line.SetPosition(0, Trail.position);
            if (Input.GetMouseButton(0) && fireTimer > fireRate)
            {
                fireTimer = 0f;
                Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
                RaycastHit hit;
                ray = new Ray(rayOrigin, cam.transform.forward);
                if (Physics.Raycast(rayOrigin, cam.transform.forward, out hit))
                {
                    line.SetPosition(1, hit.point);
                    if (hit.transform.GetComponent<Health>())
                        hit.transform.GetComponent<Health>().damage(damage);
                }
                else
                {
                    line.SetPosition(1, rayOrigin + (cam.transform.forward * range));
                }
                StartCoroutine(Shoot1());
            }

        }
    }
    IEnumerator Shoot1()
    {
        line.enabled = true;
        yield return new WaitForSeconds(duration);
        line.enabled = false;
    }
}