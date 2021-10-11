using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodIndustry : MonoBehaviour
{
    private Industry foodIndustry;

    [Header("This industry fields:")]
    [SerializeField] private GameObject _firstLVL_prefab;
    [SerializeField] private GameObject _secondLVL_prefab;

    [Header("List of statistics fields:")]
    [SerializeField] public Text NameText;
    [SerializeField] public Text LVLText;
    [SerializeField] public Text EffectivenessText;
    [SerializeField] public Text ProductionProportionText;
    [SerializeField] public Text EmployeesText;

    private Text[] _statistics;
    private string[] _industryNamesByLVL;
    private ResourceManager.Resource[] _consumableResources;
    private ResourceManager.Resource[] _producedResources;

    public ResourceManager _resourceManager;

    private void Start()
    {
        _statistics = new[] { NameText, LVLText, EffectivenessText, ProductionProportionText, EmployeesText };
        _industryNamesByLVL = new[] { "FARM" };

        foodIndustry = new Industry(Statistics: _statistics, IndustryLVLNames: _industryNamesByLVL, MAX_LVL: 1, ProductionProportion: 2, WorkplacesPerLVL: 10, IsEnabled: true);
        foodIndustry.SetNewEmployeesAmount(5);

        _consumableResources = new[] { _resourceManager.Money };
        _producedResources = new[] { _resourceManager.Food };
    }

    private void Update()
    {
        if (foodIndustry.IsEnabled)
        {
            foodIndustry.UpdateStatistics();
            foodIndustry.IndustryResourceProduction(_consumableResources, _producedResources);
        }
    }
}
