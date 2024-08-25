using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips;
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayRandom()
    {
        if (clips.Length == 0) return;

        int randIndex = Random.Range(0, clips.Length);
        source.clip = clips[randIndex];
        source.Play();
    }
}
