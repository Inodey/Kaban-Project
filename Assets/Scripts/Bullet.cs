using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletLife = 5;

    public void Awake()
    {
        Destroy(gameObject, bulletLife);
    }

    // private void OnCollisionEnter(Collision collision)
    // {
    //     Destroy(collision.gameObject);
    //     Destroy(gameObject);
    // }
}
