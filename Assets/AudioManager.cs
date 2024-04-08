using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio")]
    public GameObject audioPrefab;
    public AudioClip shootAudioClip;
    [Range(0.00f, 1.00f)]
    public float volume = 1;

    GameObject _audio;
    AudioSource _audioSource;

    void InstantiateAudioForced(){
        _audio = Instantiate(audioPrefab);
        _audioSource = _audio.GetComponent<AudioSource>();
        _audioSource.clip = shootAudioClip;
        _audioSource.volume = volume;
    }
    void InstantiateAudio(){
        if(_audio != null) return; 
        InstantiateAudioForced();
    }
    public void PlayAudio(){
        InstantiateAudio();
        if(_audioSource.isPlaying) return;
        _audioSource.Play();
    }
    public void PlayAudioOnce(){
        InstantiateAudioForced();
        _audioSource.Play();
        Destroy(_audio, shootAudioClip.length + 0.1f);
    }
    public void PauseAudio(){
        InstantiateAudio();
        if(!_audioSource.isPlaying) return;
        _audioSource.Pause();
    }
    public void UnpauseAudio(){
        InstantiateAudio();
        if(_audioSource.isPlaying) return;
        _audioSource.Play();
    }
}
