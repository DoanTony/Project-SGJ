using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AudioBank", order = 0)]
public class AudioBank : ScriptableObject {

	public static AudioBank Instance;

    public AudioSource source;
    public AudioClip stealSound;
    public AudioClip dropSound;
    public AudioClip dashSound;
    public AudioClip pickupSound;

    private void OnEnable()
    {
        if (!Instance)
        {
            Instance = FindObjectOfType<AudioBank>();
        }
        if (!Instance)
        {
            Instance = this;
        }
    }

    public void PlaySound(AudioClip clip)
    {
        AudioSource newSource = Instantiate(source);
        newSource.volume = 0.3f;
        newSource.clip = clip;
        newSource.Play();
    }
}
