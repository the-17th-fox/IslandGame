using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronIndustry : BasicIndustrialSector
{
    [SerializeField] private GameObject _firstLVL_prefab;

    [SerializeField] private Island island;

    private Resource[] ConsumableResources; // необходимые ресурсы для производства
    private Resource[] ProducedResources; // выходные ресурсы

    private BasicIndustrialSector woodIndustry;

    private void Start()
    {
        woodIndustry = gameObject.AddComponent<BasicIndustrialSector>();
        woodIndustry.CreateNewIndustry(MaxLevelConst: 1, ProductionProportion: 5, WorkplacesPerLVL: 10, isEnabled: true);
        woodIndustry.CreateArrayOfNamesByLVL("Foundry");
        woodIndustry.SetNewEmployeesAmount(10);

        ConsumableResources = new[] { island.Coal, island.IronOre, island.Money };
        ProducedResources = new[] { island.Iron };
    }

    private void Update()
    {
        if (woodIndustry.IsEnabled())
        {
            woodIndustry.IndustryResourceProduction(ConsumableResources, ProducedResources);
        }
    }
}



