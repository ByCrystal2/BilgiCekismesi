using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBehavior : MonoBehaviour
{
    [SerializeField] AudioSource m_AudioSource;
    [SerializeField] private AudioClip m_AudioClip;
    [SerializeField] VoiceType voiceType;
    [SerializeField,Range(0,1)] float Volume;
    private float StartVolume;
    private void Awake()
    {
        Debug.Log(m_AudioSource.name);
        m_AudioSource.volume = Volume;
        StartVolume = Volume;
    }
    public AudioSource GetMyAudioSource()
    {
        return m_AudioSource;
    }
    public AudioClip GetMyAudioClip()
    {
        return m_AudioClip;
    }
    public VoiceType GetMyVoiceType()
    {
        return voiceType;
    }
    public float GetMyStartVolume()
    {
        return StartVolume;
    }
    public void SetMyVolume(float _volume)
    {
        Volume = _volume;
        m_AudioSource.volume = Volume;
    }
    public void PlayMyAudio()
    {
        //AudioManager.instance.PlaySource(GetMyAudioSource(), GetMyAudioClip());
        Debug.Log(m_AudioClip.name);
        Debug.Log(m_AudioSource.name);
        m_AudioSource.clip = m_AudioClip;
        if (voiceType == VoiceType.SoundEffect)
        {
            m_AudioSource.PlayOneShot(m_AudioClip);
        }
        else if (true)
        {
            m_AudioSource.Play();
        }
        
    }
}
