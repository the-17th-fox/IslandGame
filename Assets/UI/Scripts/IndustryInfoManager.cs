using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndustryInfoManager : MonoBehaviour
{
    [Header("This industry settings:")]
    [SerializeField] private IndustryInfo _industryData;

    [Header("List of info fields:")]
    public GameObject IndustrySprite;
    public Text NameText;
    public Text LevelText;
    public Text EffectivenessText;
    public Text ProductionText;
    public Text EmployeesAmountText;

    private void Start()
    {
        IndustrySprite.GetComponent<Image>().sprite = _industryData.IndustrySprite;
    }

    private void Update()
    {
        NameText.text = _industryData.NameText;
        LevelText.text = _industryData.LevelText;
        EffectivenessText.text = _industryData.EffectivenessText;
        ProductionText.text = _industryData.ProductionText;
        EmployeesAmountText.text = _industryData.EmployeesAmountText;
    }
}
