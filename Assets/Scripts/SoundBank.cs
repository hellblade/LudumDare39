using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = "Sound/Soundbank")]
public class SoundBank : ScriptableObject
{
    [SerializeField] AudioClip[] soundOptions;
    [SerializeField] float[] volumes;

    public AudioClip GetClip(out float volume)
    {
        var index = Random.Range(0, soundOptions.Length);
        volume = volumes[index];
        return soundOptions[index];
    }

}

