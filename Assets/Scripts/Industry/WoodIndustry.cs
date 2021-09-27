using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WoodIndustry : BasicIndustrialSector
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

    private BasicIndustrialSector woodIndustry;

    private void Start()
    {
        Statistics = new[] { NameText, LVLText, EffectivenessText, ProductionProportionText, StaffingText, EmployeesText };
        ConsumableResources = new[] { island.Trees, island.Money };
        ProducedResources = new[] { island.Timber };

        woodIndustry = gameObject.AddComponent<BasicIndustrialSector>();
        woodIndustry.CreateNewIndustry(Statistics: Statistics, MaxLevelConst: 2, ProductionProportion: 1, WorkplacesPerLVL: 10, isEnabled: true, Level: 2);
        woodIndustry.CreateArrayOfNamesByLVL("FOREST HUT", "SAWMILL");
        woodIndustry.SetNewEmployeesAmount(10);
    }

    private void Update()
    {
        if (woodIndustry.IsEnabled())
        {
            woodIndustry.UpdateStatistics();
            woodIndustry.IndustryResourceProduction(ConsumableResources, ProducedResources);
        }
    }
}