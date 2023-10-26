using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Camera camera;
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;

    private void Awake()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        transform.rotation = camera.transform.rotation;
        transform.position = target.position + offset;
    }

    public void UpdateHealthbar(float curValue, float maxValue)
    {
        slider.value = curValue / maxValue;
    }
}
