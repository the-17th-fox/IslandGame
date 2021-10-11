using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WoodIndustry : MonoBehaviour
{
    private Industry woodIndustry;

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
        _industryNamesByLVL = new[] { "FOREST HUT", "SAWMILL" };

        woodIndustry = new Industry(Statistics: _statistics, IndustryLVLNames: _industryNamesByLVL, MAX_LVL: 2, ProductionProportion: 1, WorkplacesPerLVL: 10, IsEnabled: true, Level: 2);
        woodIndustry.SetNewEmployeesAmount(10);

        _consumableResources = new[] { _resourceManager.Trees, _resourceManager.Money };
        _producedResources = new[] { _resourceManager.Timber };
    }

    private void Update()
    {
        if (woodIndustry.IsEnabled)
        {
            woodIndustry.UpdateStatistics();
            woodIndustry.IndustryResourceProduction(_consumableResources, _producedResources);
        }
    }
}