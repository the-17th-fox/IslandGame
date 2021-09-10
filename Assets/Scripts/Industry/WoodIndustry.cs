using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodIndustry : MonoBehaviour
{
    [SerializeField]
    private GameObject _firstLVL_prefab;
    [SerializeField]
    private GameObject _secondLVL_prefab;
    [SerializeField]
    private GameObject _thirdLVL_prefab;
    [SerializeField]
    private GameObject _fourthLVL_prefab;

    [SerializeField]
    private byte SLevel = 1; // уровень отрасли
    [SerializeField]
    private float SEffectiveness; // коэффициент эффективности производства
    [SerializeField]
    private float SProductionProportion; // пропорция вход-выход
    [SerializeField]
    private uint SEmployeesAmount; // кол-во работников
    [SerializeField]
    private uint SWorkplacesPerLVL; // кол-во рабочих мест на каждый уровень
    [SerializeField]
    private bool isEnabledByStart; // включена ли пром-ность

    private Resource[] ConsumableResources; // необходимые ресурсы для производства
    private Resource[] ProducedResources; // выходные ресурсы

    [SerializeField]
    protected Island _island;

    BasicIndustrialSector woodIndustry;

    private void Start()
    {
        woodIndustry = gameObject.AddComponent<BasicIndustrialSector>();
            woodIndustry.CreateNewIndustry(SLevel, SEffectiveness, SProductionProportion, SEmployeesAmount, SWorkplacesPerLVL,  isEnabledByStart);

        ConsumableResources = new[] { _island.Money };
        ProducedResources = new[] { _island.Wood };
    }

    private void Update()
    {
        if (woodIndustry._isEnabled)
        {
            woodIndustry.StaffingUpdate();
            woodIndustry.IndustryResourceProduction(ConsumableResources, ProducedResources);
        }
    }
}
    


