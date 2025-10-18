using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    static public bool starHit = false;

    void OnTriggerEnter(Collider other)
    {
        Projectile proj = other.GetComponent<Projectile>();

        if (proj != null)
        {
            Star.starHit = true;
            Destroy(gameObject);
        }
    }
}
