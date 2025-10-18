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
                GameObject soundObj = new GameObject("StarSound");
                AudioSource audioSource = soundObj.AddComponent<AudioSource>();
                audioSource.clip = shinySound.clip;
                audioSource.Play();

                Destroy(soundObj, shinySound.clip.length);
            }

            MissionDemolition.StarCollected();

            Destroy(gameObject);
        }
    }
}
