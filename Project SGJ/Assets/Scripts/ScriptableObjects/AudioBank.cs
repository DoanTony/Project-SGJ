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
        Instantiate(source);
        source.volume = 0.3f;
        source.clip = clip;
        source.Play();
    }
}
