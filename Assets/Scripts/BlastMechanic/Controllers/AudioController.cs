using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : Singleton<AudioController>
{
    [Serializable]
    private class SoundEffectsRepo
    {
        public SoundEffects SoundID;
        public AudioClip AudioClip;
    }

    [SerializeField] 
    private SoundEffectsRepo[] soundIDClipPairs;

    [SerializeField] 
    private AudioSource musicSource;

    [SerializeField] 
    private AudioSource effectSource;

    private readonly Dictionary<SoundEffects, AudioClip> soundEffectToClipMap = new();

    private void Start()
    {
        InitializeSoundEffects();
    }

    private void InitializeSoundEffects()
    {
        foreach (var soundIDClipPair in soundIDClipPairs)
        {
            soundEffectToClipMap.Add(soundIDClipPair.SoundID, soundIDClipPair.AudioClip);
        }
    }

    public void PlaySoundEffect(SoundEffects soundEffect)
    {
        if (soundEffect == SoundEffects.None)
        {
             return;
        }
        
        effectSource.PlayOneShot(soundEffectToClipMap[soundEffect]);
    }

    public void StopBackgroundMusic()
    {
        musicSource.Stop();
    }
}
