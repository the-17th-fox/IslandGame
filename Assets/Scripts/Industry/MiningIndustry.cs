using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningIndustry : BasicIndustrialSector
{
    [SerializeField] private GameObject _firstLVL_prefab;

    [SerializeField] private Island island;

    private Resource[] ConsumableResources; // необходимые ресурсы для производства
    private Resource[] ProducedResources; // выходные ресурсы

    private BasicIndustrialSector ironIndustry;

    private void Start()
    {
        ironIndustry = gameObject.AddComponent<BasicIndustrialSector>();
            ironIndustry.CreateNewIndustry(MaxLevelConst: 1, ProductionProportion: 2, WorkplacesPerLVL: 10, isEnabled: true);
            ironIndustry.CreateArrayOfNamesByLVL("Mine");
            ironIndustry.SetNewEmployeesAmount(5);

        ConsumableResources = new[] { island.Money };
        ProducedResources = new[] { island.Coal, island.IronOre };
    }

    private void Update()
    {
        if (ironIndustry.IsEnabled())
        {
            ironIndustry.IndustryResourceProduction(ConsumableResources, ProducedResources);
        }
    }
}



