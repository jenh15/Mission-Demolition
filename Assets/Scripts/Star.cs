using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    static public bool starHit = false;
    public int numStars = 0;
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
            numStars++;
            if (shinySound != null)
            {
                shinySound.Play();
            }

            Destroy(gameObject, shinySound.clip.length);
        }
    }
}
