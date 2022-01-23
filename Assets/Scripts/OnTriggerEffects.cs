using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerEffects : MonoBehaviour
{
    public List<AudioClip> sounds;
    public List<string> triggers;
    private AudioSource audioSource;
    private ParticleSystem particleSystem;
    private Shake shake;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        particleSystem = GetComponent<ParticleSystem>();
        shake = Camera.main.GetComponent<Shake>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggers.Contains(collision.gameObject.tag))
        {
            int randomSoundIndex = Random.Range(0, sounds.Count - 1);
            if (audioSource != null)
            {
                audioSource.clip = sounds[randomSoundIndex];
                audioSource.Play();
            }

            if (particleSystem != null) 
            {
                particleSystem.Play();
            }

            if (shake != null)
            {
                //shake.RunShake();
            }
        }
    }
}
