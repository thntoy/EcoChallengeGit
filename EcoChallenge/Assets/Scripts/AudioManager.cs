using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _BGMSource;
    [SerializeField] private AudioSource _SFXSource;

    [SerializeField] private AudioClip _mouseClickSFX;

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _BGMSource.volume = PlayerPrefs.GetFloat("BGMVolume", 0.5f);
        _SFXSource.volume = PlayerPrefs.GetFloat("SFXVolume", 1);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlayEffect(_mouseClickSFX);
        }
    }


    public void PlayEffect(AudioClip clip)
    {
        _SFXSource.PlayOneShot(clip, 1);
    }

    public void PlayEffect(AudioClip clip, float volume = 1)
    {
        _SFXSource.PlayOneShot(clip, volume);
    }

    public void PlayEffectAtPosition(AudioClip clip, Vector3 position, float volume = 1)
    {
        _SFXSource.transform.position = position;
        this.PlayEffect(clip, volume);
    }

    public void PlayMusic(AudioClip clip)
    {
        _BGMSource.clip = clip;
        _BGMSource.Play();
    }

    public void SetMusicVolume(float value)
    {
        _BGMSource.volume = value;
        PlayerPrefs.SetFloat("BGMVolume", value);
    }
    public void SetMusicPitch(float value)
    {
        _BGMSource.pitch = value;
    }

    public void SetSFXVolume(float value)
    {
        _SFXSource.volume = value;
        PlayerPrefs.SetFloat("SFXVolume", value);
    }

    public void SetSFXPitch(float value)
    {
        _SFXSource.pitch = value;
    }
}
