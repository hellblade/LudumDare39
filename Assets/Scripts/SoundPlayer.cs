using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour
{
    [SerializeField] SoundBank[] soundBanks;
    AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlaySound(int soundbank)
    {
        float volume;
        var clip = soundBanks[soundbank].GetClip(out volume);
        source.PlayOneShot(clip, volume);
    }

}