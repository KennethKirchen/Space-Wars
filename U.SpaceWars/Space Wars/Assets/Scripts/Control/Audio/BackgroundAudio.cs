using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAudio : MonoBehaviour
{
    void Start()
    {
        if (GameObject.Find("Background Audio") != gameObject) Destroy(gameObject);

        EventManager.Instance.ChangeMusicVolumeEvent += ChangeVolume;
    }

    private void OnDestroy()
    {
        EventManager.Instance.ChangeMusicVolumeEvent -= ChangeVolume;
    }

    void ChangeVolume()
    {
        if (GetComponent<AudioSource>() == null) return;

        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Music Volume");
    }
}
