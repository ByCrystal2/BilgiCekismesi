using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    List<SoundBehavior> _allAudioSources = new List<SoundBehavior>();
    List<SoundBehavior> _musicSources = new List<SoundBehavior>();
    List<SoundBehavior> _soundEffectSources = new List<SoundBehavior>();

    [SerializeField] AudioSource M_AudioSource;
    [SerializeField] SoundBehavior M_GameClip;
    [SerializeField] SoundBehavior M_MenuClip;
    public static AudioManager instance { get; private set; }
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void PlayMenuClip()
    {
        M_GameClip.GetMyAudioSource().Stop();
        M_MenuClip.PlayMyAudio();
    }
    public void PlayGameClip()
    {
        M_MenuClip.GetMyAudioSource().Stop();
        M_GameClip.PlayMyAudio();
    }
    public void SetAllAudioSources()
    {
        ClearSources();
        _allAudioSources = FindObjectsOfType<SoundBehavior>().ToList();
        SetMusicSources();
        SetSoundEffectSources();
    }
    private void ClearSources()
    {
        _allAudioSources.Clear();
        _musicSources.Clear();
        _soundEffectSources.Clear();
    }
    private void SetMusicSources()
    {
        _musicSources = _allAudioSources.Where(x=> x.GetMyVoiceType() == VoiceType.Music).ToList();
    }
    private void SetSoundEffectSources()
    {
        _soundEffectSources = _allAudioSources.Where(x=> x.GetMyVoiceType() == VoiceType.SoundEffect).ToList();
    }
    public void PlaySource(AudioSource _source, AudioClip _clip)
    {
        _source.Stop();
        _source.clip = _clip;
        _source.Play();
    }
    public void StopAllSource()
    {
        foreach (var ASource in _allAudioSources)
        {
            ASource.GetMyAudioSource().Pause();
        }
    }
    public void PlayAllSource()
    {
        foreach (var ASource in _allAudioSources)
        {
            ASource.GetMyAudioSource().UnPause();
        }
    }
    public void StopMusicSources()
    {
        foreach (var ASource in _musicSources)
        {
            ASource.GetMyAudioSource().Pause();
        }
    }
    public void PlayMusicSources()
    {
        foreach (var ASource in _musicSources)
        {
            ASource.GetMyAudioSource().UnPause();
        }
    }
    public void StopSoundEffectSource()
    {
        foreach (var ASource in _soundEffectSources)
        {
            ASource.GetMyAudioSource().Pause();
        }
    }
    public void PlaySoundEffectSource()
    {
        foreach (var ASource in _soundEffectSources)
        {
            ASource.GetMyAudioSource().UnPause();
        }
    }
    public void TurnDownAllSources(float _sliderValue)
    {
        foreach (var ASource in _allAudioSources)
        {
            ASource.SetMyVolume(_sliderValue);
        }
    }
    public void TurnDownMusicSources(float _sliderValue)
    {
        foreach (var ASource in _musicSources)
        {
            ASource.SetMyVolume(_sliderValue);
        }
    }
    public void TurnDownSoundEffectSources(float _sliderValue)
    {
        foreach (var ASource in _musicSources)
        {
            ASource.SetMyVolume(_sliderValue);
        }
    }
    public void RestartAllSouurces()
    {
        foreach (var ASource in _allAudioSources)
        {
            ASource.SetMyVolume(ASource.GetMyStartVolume());
        }
    }
}
public enum VoiceType
{
    None,
    Music,
    SoundEffect
}
