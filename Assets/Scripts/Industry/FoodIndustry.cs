using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodIndustry : BasicIndustrialSector
{
    [SerializeField] private GameObject _firstLVL_prefab;

    [SerializeField] private Island island;

    private Resource[] ConsumableResources; // необходимые ресурсы для производства
    private Resource[] ProducedResources; // выходные ресурсы

    private BasicIndustrialSector foodIndustry;

    private void Start()
    {
        foodIndustry = gameObject.AddComponent<BasicIndustrialSector>();
        foodIndustry.CreateNewIndustry(MaxLevelConst: 1, ProductionProportion: 2, WorkplacesPerLVL: 10, isEnabled: true);
        foodIndustry.CreateArrayOfNamesByLVL("Farm");
        foodIndustry.SetNewEmployeesAmount(5);

        ConsumableResources = new[] { island.Money };
        ProducedResources = new[] { island.Food };
    }

    private void Update()
    {
        if (foodIndustry.IsEnabled())
        {
            foodIndustry.IndustryResourceProduction(ConsumableResources, ProducedResources);
        }
    }
}



