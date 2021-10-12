using UnityEngine;
using UnityEngine.UI;

public class WoodIndustry : MonoBehaviour
{
    private IndustryCore woodIndustry;

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
        //_statistics = new[] { NameText, LVLText, EffectivenessText, ProductionProportionText, EmployeesText };
        _industryNamesByLVL = new[] { "FOREST HUT", "SAWMILL" };

        woodIndustry = new IndustryCore(IndustryInfo: _industryInfo, IndustryLVLNames: _industryNamesByLVL, MAX_LVL: 2, ProductionProportion: 1, WorkplacesPerLVL: 10, IsEnabled: true, Level: 2);

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