using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;

public class SliderScript : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Text _sliderValueText;

    [SerializeField] Image _fillColor;

    private void Start()
    {
        _slider.onValueChanged.AddListener((v) =>
        {
            _fillColor.color = Color.Lerp(Color.red, Color.green, _slider.value / 100 + 0.2f);
            _sliderValueText.text = $"{Math.Round(v,1)}%";
        });
    }
}
