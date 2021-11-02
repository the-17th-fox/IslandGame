using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SliderScript : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Text _sliderValueText;

    [SerializeField] Image _fill;
    //[SerializeField] private Color _Full;
    //[SerializeField] private Color _50upTo75Color;
    //[SerializeField] private Color _25upTo50Color = Color.He;
    //[SerializeField] private Color _0upTo25Color = Color.red;

    private void Start()
    {
        _slider.onValueChanged.AddListener((v) =>
        {
            _fill.color = Color.Lerp(Color.red, Color.green, _slider.value / 100 + 0.2f);


            _sliderValueText.text = $"{v.ToString("0")} %";
        });
    }
}
