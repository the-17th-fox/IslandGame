using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodIndustry : BasicIndustrialSector
{
    [SerializeField] private GameObject _firstLVL_prefab;
    [SerializeField] private GameObject _secondLVL_prefab;

    [SerializeField] private Island island;

    private Resource[] ConsumableResources; // необходимые ресурсы для производства
    private Resource[] ProducedResources; // выходные ресурсы

    private BasicIndustrialSector woodIndustry;

    private void Start()
    {
        woodIndustry = gameObject.AddComponent<BasicIndustrialSector>();
            woodIndustry.CreateNewIndustry(MaxLevelConst: 2, ProductionProportion: 1,  WorkplacesPerLVL: 10, isEnabled: true, Level: 1); // stuffing 0.5
            woodIndustry.CreateArrayOfNamesByLVL("Forest Hut", "Sawmill");
            woodIndustry.SetNewEmployeesAmount(10);

        ConsumableResources = new[] { island.Trees, island.Money };
        ProducedResources = new[] { island.Timber };
    }

    private void Update()
    {
        if (woodIndustry.IsEnabled())
        {
            woodIndustry.IndustryResourceProduction(ConsumableResources, ProducedResources);
        }
    }
}
    


