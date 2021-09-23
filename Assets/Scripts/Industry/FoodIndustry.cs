using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class FoodIndustry : BasicIndustrialSector
{
    [Header("This industry fields:")]
    [SerializeField] private GameObject _firstLVL_prefab;
    [SerializeField] private GameObject _secondLVL_prefab;
    [SerializeField] private Island island;

    [Header("List of statistics fields:")]
    [SerializeField] private Text NameText;
    [SerializeField] private Text LVLText;
    [SerializeField] private Text EffectivenessText;
    [SerializeField] private Text ProductionProportionText;
    [SerializeField] private Text StaffingText;
    [SerializeField] private Text EmployeesText;

    private Text[] Statistics;
    private Resource[] ConsumableResources; // необходимые ресурсы для производства
    private Resource[] ProducedResources; // выходные ресурсы

    private BasicIndustrialSector foodIndustry;

    private void Start()
    {
        Statistics = new[] { NameText, LVLText, EffectivenessText, ProductionProportionText, StaffingText, EmployeesText };
        ConsumableResources = new[] { island.Money };
        ProducedResources = new[] { island.Food };

        foodIndustry = gameObject.AddComponent<BasicIndustrialSector>();
        foodIndustry.CreateNewIndustry(Statistics: Statistics, MaxLevelConst: 1, ProductionProportion: 2, WorkplacesPerLVL: 10, isEnabled: true);
        foodIndustry.CreateArrayOfNamesByLVL("Farm");
        foodIndustry.SetNewEmployeesAmount(5);
    }

    private void Update()
    {
        if (foodIndustry.IsEnabled())
        {
            foodIndustry.UpdateStatistics();
            foodIndustry.IndustryResourceProduction(ConsumableResources, ProducedResources);
        }
    }
}
