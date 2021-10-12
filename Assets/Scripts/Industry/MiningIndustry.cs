using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiningIndustry : MonoBehaviour
{
    private IndustryCore miningIndustry;

    [Header("This industry fields:")]
    [SerializeField] private List<GameObject> _firstLVL_prefab;

    [Header("Data object:")]
    [SerializeField] private IndustryInfo _industryInfo;

    private string[] _industryNamesByLVL;
    private ResourceManager.Resource[] _consumableResources;
    private ResourceManager.Resource[] _producedResources;

    public ResourceManager _resourceManager;

    private void Start()
    {
        _industryNamesByLVL = new[] { "MINE" };

        miningIndustry = new IndustryCore(ProductionProportion: 1, MAX_LVL: 1, IndustryInfo: _industryInfo, IndustryLVLNames: _industryNamesByLVL);

        _consumableResources = new[] { _resourceManager.Money };
        _producedResources = new[] { _resourceManager.Coal, _resourceManager.IronOre };
    }

    private void Update()
    {
        if (miningIndustry.IsEnabled)
        {
            miningIndustry.UpdateStatistics();
            miningIndustry.IndustryResourceProduction(_consumableResources, _producedResources);
        }
    }
}
