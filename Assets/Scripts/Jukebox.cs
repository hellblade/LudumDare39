using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Jukebox : MonoBehaviour
{
    private AudioSource audioSource;
    public new AudioClip[] audio;

    static bool loaded = false;

    private void Awake()
    {
        if (loaded)
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(this);
        loaded = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = audio[Random.Range(0, audio.Length)];
            audioSource.Play();
        }
    }
}