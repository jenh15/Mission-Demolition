using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    static public bool starHit = false;
    private AudioSource shinySound;

    void Start()
    {
        shinySound = GetComponent<AudioSource>();
    }
    void OnTriggerEnter(Collider other)
    {
        Projectile proj = other.GetComponent<Projectile>();

        if (proj != null)
        {
            Star.starHit = true;
            if (shinySound != null)
            {
                shinySound.Play();
            }

            MissionDemolition.StarCollected();

            Destroy(gameObject, shinySound.clip.length);
        }
    }
}
