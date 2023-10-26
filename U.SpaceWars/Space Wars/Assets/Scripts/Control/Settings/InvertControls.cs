using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvertControls : MonoBehaviour
{
    [SerializeField] Toggle invertControlsToggle;

    private void Awake()
    {
        if (PlayerPrefs.GetInt(Constants.INVERT_CONTROLS_SAVE_STRING) == 0)
        {
            invertControlsToggle.isOn = false;
        }
        else invertControlsToggle.isOn = true;
    }

    public void ChangeControls()
    {
        int invertControlsSaveIndex = 0;
        if (invertControlsToggle.isOn) invertControlsSaveIndex = 1;

        PlayerPrefs.SetInt(Constants.INVERT_CONTROLS_SAVE_STRING, invertControlsSaveIndex);
        EventManager.Instance.InvokeInvertControlsEvent();
    }
}
