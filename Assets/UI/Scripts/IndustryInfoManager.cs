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

    private void SetNewEmployeesAmount(int value)
    {
        int MAX_WORKPLACES = _industryData.Level * _industryData.WorkplacesPerLVL;

        if (value < IndustryCore.MIN_WORKPLACES)
            _industryData.EmployeesAmount = (int)IndustryCore.MIN_WORKPLACES;
        else if (value > MAX_WORKPLACES)
            _industryData.EmployeesAmount = MAX_WORKPLACES;
        else
            _industryData.EmployeesAmount = value;
    }

    private void Awake()
    {
        IndustrySprite.GetComponent<Image>().sprite = _industryData.IndustrySprite;
        NameText.text = _industryData.NameText;
        LevelText.text = _industryData.LevelText;
        EffectivenessText.text = _industryData.EffectivenessText;
        ProductionText.text = _industryData.ProductionText;
        EmployeesAmountText.text = _industryData.EmployeesAmountText;
    }

    private void Update()
    {
        NameText.text = _industryData.NameText;
        LevelText.text = _industryData.LevelText;
        EffectivenessText.text = _industryData.EffectivenessText;
        ProductionText.text = _industryData.ProductionText;
        //_industryData.EmployeesAmount = _newEmployeesAmount;
        EmployeesAmountText.text = _industryData.EmployeesAmountText;
    }

    public void IncreaseEmployeesAmount()
    {
        int freeEmployees = (int)(Population._WorkablePopulation - Population._EmployedPopulation);
        int workplacesAmount = _industryData.Level * _industryData.WorkplacesPerLVL;

        if (freeEmployees >= 1 && _industryData.EmployeesAmount + 1 <= workplacesAmount)
        {
            if (_industryData.EmployeesAmount + 1 >= workplacesAmount)
            {
                SetNewEmployeesAmount(workplacesAmount);
            }
            else if (_industryData.EmployeesAmount + 1 < workplacesAmount)
            {
                SetNewEmployeesAmount(_industryData.EmployeesAmount + 1);
            }

            Population._EmployedPopulation++;
        }
    }

    public void DecreaseEmployeesAmount()
    {
        if (_industryData.EmployeesAmount - 1 >= 0)
        {
            SetNewEmployeesAmount(_industryData.EmployeesAmount - 1);

            Population._EmployedPopulation--;
        }
    }
}
