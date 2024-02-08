using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsPanelController : MonoBehaviour
{
    [SerializeField] Slider MusicSlider;
    [SerializeField] Slider SoundEffectSlider;

    [SerializeField] Toggle MusicToggle;
    [SerializeField] Toggle SoundEffectToggle;

    [SerializeField] Button ApplyButton;
    [SerializeField] Button RestartButton;
    [SerializeField] Button CancelButton;

    

    private void Awake()
    {

    }
    public void ApplyOptions()
    {
        if (MusicToggle.isOn)
        {
            AudioManager.instance.StopMusicSources();
            Debug.Log("Music Stopped: " + MusicToggle.isOn);
        }
        else
        {
            AudioManager.instance.PlayMusicSources();
            AudioManager.instance.TurnDownMusicSources(MusicSlider.value);
        }
        if (SoundEffectToggle.isOn)
        {
            AudioManager.instance.StopSoundEffectSource();
        }
        else
        {
            AudioManager.instance.PlaySoundEffectSource();
            AudioManager.instance.TurnDownSoundEffectSources(SoundEffectSlider.value);
        }
        UIManager.instance.OptionsPanelActivation(false);
    }
    public void RestartOptions()
    {
        AudioManager.instance.RestartAllSouurces();
        MusicSlider.value = 1f;
        SoundEffectSlider.value = 1f;

        MusicToggle.isOn = false;
        SoundEffectToggle.isOn = false;
        ApplyOptions();
    }
    public void CancelOptions()
    {
        UIManager.instance.OptionsPanelActivation(false);
    }
}
