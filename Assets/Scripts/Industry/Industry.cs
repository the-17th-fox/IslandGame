using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Industry : MonoBehaviour
{
    [SerializeField] private IndustryInfo _industryInfo;

    private IndustryCore _industry;

    private ResourceManager.Resource[] _producibleResources;
    private ResourceManager.Resource[] _consumableResources;

    private ResourceManager.Resource[] _resources = new[] { ResourceManager.Timber, ResourceManager.Food, ResourceManager.Iron, ResourceManager.IronOre,
                                                           ResourceManager.Coal, ResourceManager.Money, ResourceManager.Trees };

    void Start()
    {
        _producibleResources = new ResourceManager.Resource[_industryInfo._producibleResourcesIndexes.Length];

        for (int i = 0; i < _industryInfo._producibleResourcesIndexes.Length; i++)
        {
            _producibleResources[i] = _resources[_industryInfo._producibleResourcesIndexes[i]];
        }

        _consumableResources = new ResourceManager.Resource[_industryInfo._consumableResourcesIndexes.Length];

        for (int i = 0; i < _industryInfo._consumableResourcesIndexes.Length; i++)
        {
            _consumableResources[i] = _resources[_industryInfo._consumableResourcesIndexes[i]];
        }

        _industry = new IndustryCore
            (
            ProductionProportion: _industryInfo.ProductionProportion,
            MAX_LVL: _industryInfo.MAX_LVL,
            IndustryInfo: _industryInfo,
            IndustryLVLNames: _industryInfo.IndustryNamesByLevel,
            WorkplacesPerLVL: _industryInfo.WorkplacesPerLVL,
            Effectiveness: _industryInfo.Effectiveness,
            Level: _industryInfo.Level,
            IsEnabled: _industryInfo.IsEnabled,
            DebugLog: _industryInfo.DebugLog
            );
    }

    void Update()
    {
        if (_industry.DebugLog)
            _industry.GetDebugLog();

        _industry.EmployeesRecalculation();


        _industry.ResourceProduction(ConsumableResources: _consumableResources, ProducedResources: _producibleResources);
        _industry.UpdateStatistics();
    }
}
