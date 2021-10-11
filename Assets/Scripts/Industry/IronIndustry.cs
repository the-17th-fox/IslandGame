using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IronIndustry : MonoBehaviour
{
    private Industry ironIndustry;

    [Header("This industry fields:")]
    [SerializeField] private GameObject _firstLVL_prefab;

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
        _industryNamesByLVL = new[] { "FOUNDRY" };

        ironIndustry = new Industry(Statistics: _statistics, IndustryLVLNames: _industryNamesByLVL, MAX_LVL: 1, ProductionProportion: 0.5f, WorkplacesPerLVL: 10, IsEnabled: true);
        ironIndustry.SetNewEmployeesAmount(10);

        _consumableResources = new[] { _resourceManager.Coal, _resourceManager.IronOre, _resourceManager.Money };
        _producedResources = new[] { _resourceManager.Iron };
    }

    private void Update()
    {
        if (ironIndustry.IsEnabled)
        {
            ironIndustry.UpdateStatistics();
            ironIndustry.IndustryResourceProduction(_consumableResources, _producedResources);
        }
    }
}