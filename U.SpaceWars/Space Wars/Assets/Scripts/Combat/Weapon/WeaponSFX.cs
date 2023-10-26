using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSFX : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    private void Awake()
    {
        ChangeVolume();
    }

    private void Start()
    {
        EventManager.Instance.PlayerWeaponFireSFXEvent += PlaySound;
        EventManager.Instance.ChangeSFXVolumeEvent += ChangeVolume;
    }

    private void OnDestroy()
    {
        EventManager.Instance.PlayerWeaponFireSFXEvent -= PlaySound;
    }

    void PlaySound()
    {
        if (audioSource.isPlaying) StopPlayingSound();

        audioSource.Play();
    }

    void StopPlayingSound()
    {
        audioSource.Stop();
    }

    void ChangeVolume()
    {
        audioSource.volume = PlayerPrefs.GetFloat("SFX Volume");
    }
}
