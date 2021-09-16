using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronIndustry : BasicIndustrialSector
{
    [SerializeField]
    private GameObject _firstLVL_prefab;
    [SerializeField]
    private GameObject _secondLVL_prefab;
    [SerializeField]
    private GameObject _thirdLVL_prefab;
    [SerializeField]
    private GameObject _fourthLVL_prefab;

    private Resource[] ConsumableResources; // необходимые ресурсы для производства
    private Resource[] ProducedResources; // выходные ресурсы

    [SerializeField]
    private Island island;

    private BasicIndustrialSector ironIndustry;

    private void Start()
    {
        ironIndustry = gameObject.AddComponent<BasicIndustrialSector>();
            ironIndustry.CreateNewIndustry(MaxLevelConst: 1, ProductionProportion: 2, WorkplacesPerLVL: 5, isEnabled: true);
            ironIndustry.CreateArrayOfNamesByLVL("Iron Mine and Furnace");
            ironIndustry.SetNewEmployeesAmount(5);

        ConsumableResources = new[] { island.Money, island.Timber };
        ProducedResources = new[] { island.Iron };
    }

    private void Update()
    {
        if (ironIndustry.IsEnabled())
        {
            ironIndustry.IndustryResourceProduction(ConsumableResources, ProducedResources);
        }
    }
}



