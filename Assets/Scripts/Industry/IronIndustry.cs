using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IronIndustry : BasicIndustrialSector
{
    [Header("This industry fields:")]
    [SerializeField] private GameObject _firstLVL_prefab;
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

    private BasicIndustrialSector ironIndustry;

    private void Start()
    {
        Statistics = new[] { NameText, LVLText, EffectivenessText, ProductionProportionText, StaffingText, EmployeesText };
        ConsumableResources = new[] { island.Coal, island.IronOre, island.Money };
        ProducedResources = new[] { island.Iron };

        ironIndustry = gameObject.AddComponent<BasicIndustrialSector>();
        ironIndustry.CreateNewIndustry(Statistics: Statistics, MaxLevelConst: 1, ProductionProportion: 0.5f, WorkplacesPerLVL: 10, isEnabled: true);
        ironIndustry.CreateArrayOfNamesByLVL("Foundry");
        ironIndustry.SetNewEmployeesAmount(10);
    }

    private void Update()
    {
        if (ironIndustry.IsEnabled())
        {
            ironIndustry.UpdateStatistics();
            ironIndustry.IndustryResourceProduction(ConsumableResources, ProducedResources);
        }
    }
}