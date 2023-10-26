using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] Slider sFXVolumeSlider;
    [SerializeField] Slider musicVolumeSlider;

    private void Awake()
    {
        PlayerPrefs.SetFloat(Constants.SFX_VOLUME_SAVE_STRING, 0.5f);
        PlayerPrefs.SetFloat(Constants.MUSIC_VOLUME_SAVE_STRING, 0.5f);
    }

    public void ChangeSFXVolume()
    {
        PlayerPrefs.SetFloat(Constants.SFX_VOLUME_SAVE_STRING, sFXVolumeSlider.value);
        EventManager.Instance.InvokeChangeSFXVolumeEvent();
    }

    public void ChangeBackgroundMusicVolume()
    {
        PlayerPrefs.SetFloat(Constants.MUSIC_VOLUME_SAVE_STRING, musicVolumeSlider.value);
        EventManager.Instance.InvokeChangeMusicVolumeEvent();
    }

    public void SetSliderValues()
    {

        sFXVolumeSlider.value = PlayerPrefs.GetFloat(Constants.SFX_VOLUME_SAVE_STRING);
        musicVolumeSlider.value = PlayerPrefs.GetFloat(Constants.MUSIC_VOLUME_SAVE_STRING);

    }
}
