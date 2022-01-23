using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicPlayer : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioClip musicClip;

    private AudioSource audioSource;

    private static MusicPlayer _instance;
    public static MusicPlayer Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(_instance);
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = musicClip;
        audioSource.loop = true;
        AudioListener.volume = volumeSlider.value;
        //audioSource.volume = volumeSlider.value;
        audioSource.Play();
    }

    private void Update()
    {
        AudioListener.volume = volumeSlider.value;
    }
}
