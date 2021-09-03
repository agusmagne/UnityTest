using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleEnemyHP : MonoBehaviour
{
    private Rigidbody body;
    private float hp = 3;
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (hp == 1)
        {
            Destroy(gameObject, 1f);
        }
        else
        {
            Vector3 impactDirection = collision.relativeVelocity.normalized;
            impactDirection.y = 0f;
            body.velocity = impactDirection * 5;
            hp--;
        }
    }
}
