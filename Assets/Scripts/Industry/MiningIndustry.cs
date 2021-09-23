using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiningIndustry : BasicIndustrialSector
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

    private BasicIndustrialSector miningIndustry;

    private void Start()
    {
        Statistics = new[] { NameText, LVLText, EffectivenessText, ProductionProportionText, StaffingText, EmployeesText };
        ConsumableResources = new[] { island.Money };
        ProducedResources = new[] { island.Coal, island.IronOre };

        miningIndustry = gameObject.AddComponent<BasicIndustrialSector>();
        miningIndustry.CreateNewIndustry(Statistics: Statistics, MaxLevelConst: 1, ProductionProportion: 5, WorkplacesPerLVL: 10, isEnabled: true);
        miningIndustry.CreateArrayOfNamesByLVL("Mine");
        miningIndustry.SetNewEmployeesAmount(5);
    }

    private void Update()
    {
        if (miningIndustry.IsEnabled())
        {
            miningIndustry.UpdateStatistics();
            miningIndustry.IndustryResourceProduction(ConsumableResources, ProducedResources);
        }
    }
}
