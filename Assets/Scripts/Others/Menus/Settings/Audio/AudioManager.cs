using UnityEngine;
using System;

internal class AudioManager : MonoBehaviour {
    [SerializeField] Audio[] soundList;

    AudioSource audioSource => GetComponent<AudioSource>();

    Audio PlaySound(string name) {
        if (DebuggingAudioSource(audioSource))
            return null;

        Audio sound = Array.Find(soundList, sound => sound.name == name);

        if (sound == null) {
            Debug.LogWarning("Audio: " + name + " not found!");

            return null;
        }

        audioSource.clip = sound.clip;
        audioSource.volume = sound.volume;
        audioSource.pitch = sound.pitch;
        audioSource.loop = sound.loop;

        return sound;
    }

    public void PlayMusic() {
        if(DebuggingSound(audioSource))
            return;

        audioSource.Play();
    }

    internal void PlayOneShot(string name) {
        audioSource.PlayOneShot(PlaySound(name).clip);
    }

    public void StopMusic() {
        if(DebuggingSound(audioSource))
            return;

        audioSource.Stop();
    }

    bool DebuggingAudioSource(AudioSource audioSource) {
        bool NotFound = false;

        if (audioSource == null) {
            Debug.LogWarning("Audio Source: " + audioSource.name + " not found!");
            NotFound = true;
        }

        return NotFound;
    }

    bool DebuggingSound(AudioSource audioSource) {
        bool NotFound = false;

        if(audioSource.clip == null) {
            Debug.LogWarning("Sound: not found!");
            NotFound = true;
        }

        return NotFound;
    }
}
