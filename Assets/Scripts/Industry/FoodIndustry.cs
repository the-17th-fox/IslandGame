using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodIndustry : MonoBehaviour
{
    private IndustryCore foodIndustry;

    [Header("This industry fields:")]
    [SerializeField] private GameObject _firstLVL_prefab;
    [SerializeField] private GameObject _secondLVL_prefab;

    [Header("Data object:")]
    [SerializeField] private IndustryInfo _industryInfo;

    private string[] _industryNamesByLVL;
    private ResourceManager.Resource[] _consumableResources;
    private ResourceManager.Resource[] _producedResources;

    public ResourceManager _resourceManager;

    private void Start()
    {
        _industryNamesByLVL = new[] { "FARM" };

        foodIndustry = new IndustryCore(IndustryInfo: _industryInfo, IndustryLVLNames: _industryNamesByLVL, MAX_LVL: 1, ProductionProportion: 2, WorkplacesPerLVL: 10, IsEnabled: true);

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
