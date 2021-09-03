using System.Collections;
using System.Collections.Generic;
using DevilsDen.Events;
using UnityEngine;

public class OrbitPlayer : MonoBehaviour
{
    public int direction;

    GameObject player;
    Transform cam;
    Rigidbody rb;
    Vector3 preparingGrenadePosition;
    Vector3 preparingGrenadeDestination;
    float timeCorrection;
    float orbitVelocity = 3.0f;
    bool isThrowing = false;
    float throwVelocity;

    void Awake()
    {
        EventManager.StartListening(AbilitiesDictionary.grenade, OnThrowGrenade);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        cam = Camera.main.transform;
        player = transform.parent.gameObject;
        transform.SetParent(null);
        timeCorrection = Time.time;
    }

    void Update()
    {
        if (!isThrowing)
        {
            Vector3 relativePos = player.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            Quaternion current = transform.localRotation;
            transform.localRotation = Quaternion.Slerp(current, rotation, Time.deltaTime * orbitVelocity);

            Vector3 pos = player.transform.position - (-transform.right) * direction;
            pos.y = Mathf.Sin(Time.time * orbitVelocity - timeCorrection) + 1.7f;
            transform.position = pos;
        }
    }

    void OnThrowGrenade(Dictionary<string, object> args)
    {
        throwVelocity = (float)args[AbilitiesDictionary.grenadeVelocity];
        GameObject obj = (GameObject)args[AbilitiesDictionary.grenadeObject];
        if (obj == gameObject)
        {
            EventManager.StopListening(AbilitiesDictionary.grenade, OnThrowGrenade);
            StartCoroutine(Explode());
        }
    }

    IEnumerator Explode()
    {
        Debug.Log(throwVelocity);
        isThrowing = true;
        rb.useGravity = true;

        Vector3 vector = cam.forward;
        vector.y = 0f;
        vector.Normalize();
        vector *= throwVelocity;
        vector = Quaternion.AngleAxis(-15, cam.right) * vector;
        transform.position = player.transform.position + cam.forward;
        rb.velocity = vector;

        yield return new WaitForSeconds(3f);

        Destroy(gameObject);
    }
}
