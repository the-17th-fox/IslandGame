using UnityEngine;
using UnityEngine.UI;

public class IronIndustry : MonoBehaviour
{
    private IndustryCore ironIndustry;

    [Header("This industry fields:")]
    [SerializeField] private GameObject _firstLVL_prefab;

    [Header("Data object:")]
    [SerializeField] private IndustryInfo _industryInfo;

    private string[] _industryNamesByLVL;
    private ResourceManager.Resource[] _consumableResources;
    private ResourceManager.Resource[] _producedResources;

    public ResourceManager _resourceManager;

    private void Start()
    {
        _industryNamesByLVL = new[] { "FOUNDRY" };

        ironIndustry = new IndustryCore(IndustryInfo: _industryInfo, IndustryLVLNames: _industryNamesByLVL, MAX_LVL: 1, ProductionProportion: 0.5f, WorkplacesPerLVL: 10, IsEnabled: true);

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