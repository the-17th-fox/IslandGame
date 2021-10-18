using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Population : MonoBehaviour
{
    [SerializeField] private static float _TotalAmount = 1;// количество населения
    [SerializeField] public static float _EducationLevel;// образованность
    [SerializeField] private static float _EducationSupply;//расчетный уровень образования
    [SerializeField] private static float _GenerationSpeed;//естественный прирост (хз вроде не юзается)
    [SerializeField] public static float _MedicineLevel;// реальный уровень медицины
    [SerializeField] private static float _MedicineSupply;//расчетный уровень медецины
    [SerializeField] private static float _NecessarySatisfactedNeeds;// удавлетворенные базовые нужды
    //public static float _LuxurySatisfactedNeeds;
    [SerializeField] private static float _BasicPopulationIncreaseRate; // естественный прирост 
    //public static float _ImmigrationAttractiveness=1;// перерасчет добавить
    [SerializeField] public static float _EmployablePopulation; // работоспособное население
    private const float _NecessaryNeeds = 0.001f;// константа нужда базовых поребностей на человека
    private const float _EducationBugetPerPerson = 0.005f; // трата на образование на человека для max образованности
    private const float _MedcineBudgetPerPerson = 0.01f; // трата на медицину на человека для max образованности
    private const float _BasePartEmployablePopulation = 0.7f; //базово работает 

    [Header("List of text fields:")]
    [SerializeField] private static Text PopulationAmountText;
    [SerializeField] private static Text EducationLevelText;
    [SerializeField] private static Text MedicineLevelText;
    [SerializeField] private static Text PopulationIncreaseRateText;
    [SerializeField] private static Text NecessarySatisfactedNeedsText;

    private static ResourceManager.Resource[] _ConsumableResources;
    //private static Population population;

    private void Start()
    {
        //island = gameObject.AddComponent<Island>();
        //population = gameObject.AddComponent<Population>();
        CreatePopulation(Amount: 1000, Education: 0.5f, Medcine: 0.5f);
        _ConsumableResources = new[] { ResourceManager.Food };
        _MedicineSupply = 1f;
        _EducationSupply = 1f;
    }
    private void Update()
    {
        if (Timer.SecondGone())
        {
            Debug.Log($"P{_TotalAmount} F: {ResourceManager.Food.Amount} M: {_MedicineLevel} Money:{ResourceManager.Money.Amount},E:{_EducationLevel},WP:{_EmployablePopulation}");
            PopulationNeedUpdate(_ConsumableResources);
            MedicineUpdate();
            EducationUpdate();
            PopulationIncrease();
            EmployablePopulationUpdate();
        }
        StatisticsUpdate();
    }

    private static void CreatePopulation(uint Amount,float Education, float Medcine) 
    {
        _TotalAmount = Amount;
        _EducationLevel = Education;
        _MedicineLevel = Medcine;
        _NecessarySatisfactedNeeds = 1f;  //со старта нужд нет
        //_LuxurySatisfactedNeeds = 1;
        _BasicPopulationIncreaseRate = 0.01f;
    }
    private static void PopulationNeedUpdate(ResourceManager.Resource[] NessaryResources/*, Resource[] LuxuryResourse*/) 
    {
        _NecessarySatisfactedNeeds = PopulationNeed(NessaryResources);
        //_LuxurySatisfactedNeeds = PopulationNeed(LuxuryResourse);
    }
    private static void PopulationIncrease() 
    {
        _TotalAmount += (_BasicPopulationIncreaseRate * _TotalAmount * (-0.5f+_MedicineLevel  + _NecessarySatisfactedNeeds)) /** Time.deltaTime*/;   
    }
    
    private static float PopulationNeed(ResourceManager.Resource[] ConsumableResources) 
    {
        float NeedValue = _NecessaryNeeds * _TotalAmount; // 1
        float TotalNeedSatisfation = 0; 
        for(int i=0; i < ConsumableResources.Length; i++)
        {
            if (ConsumableResources[i].Amount >= NeedValue)
            {
                TotalNeedSatisfation++;
                ConsumableResources[i].Amount -= NeedValue /** Time.deltaTime*/;
            }
            else 
            {
                TotalNeedSatisfation += ConsumableResources[i].Amount / NeedValue;
                ConsumableResources[i].Amount -= ConsumableResources[i].Amount /** Time.deltaTime*/;
            }
        }
        return (TotalNeedSatisfation / ConsumableResources.Length);
    }
    private static void MedicineUpdate() 
    {
        float PartMedcineSupply = 1;
        if (ResourceManager.Money.Amount >= _MedicineSupply * _MedcineBudgetPerPerson * _TotalAmount)
        {
            ResourceManager.Money.Amount -= _MedicineSupply * _MedcineBudgetPerPerson * _TotalAmount;//списание денег за медицину
        }
        else 
        {
            PartMedcineSupply = ResourceManager.Money.Amount / (_MedicineSupply * _MedcineBudgetPerPerson * _TotalAmount);
            ResourceManager.Money.Amount = 0;
        }

        float DeltaSupply = (PartMedcineSupply * _MedicineSupply)-_MedicineLevel;//узнаем разницу уровня реальной медицины от оплаченной
        if (DeltaSupply < 0.03f && DeltaSupply > -0.03f)
        {
            _MedicineLevel = PartMedcineSupply* _MedicineSupply;
        }
        else
        {
            _MedicineLevel += DeltaSupply / 10;// зачисляется 10% до расчитываемого уровня
        }
    }
    private static void EducationUpdate()
    {
        float PartEducationSupply = 1;
        if (ResourceManager.Money.Amount >= _EducationSupply * _EducationBugetPerPerson * _TotalAmount)
        {
            ResourceManager.Money.Amount -= _EducationSupply * _EducationBugetPerPerson * _TotalAmount;//списание денег за образование
        }
        else
        {
            PartEducationSupply = ResourceManager.Money.Amount / (_EducationSupply * _EducationBugetPerPerson * _TotalAmount);
            ResourceManager.Money.Amount = 0;
        }

        float DeltaSupply = (PartEducationSupply * _EducationSupply) - _EducationLevel;//узнаем разницу уровня реального образования от оплаченного
        if (DeltaSupply < 0.03f && DeltaSupply > -0.03f)
        {
            _EducationLevel = PartEducationSupply * _EducationSupply;
        }
        else
        {
            _EducationLevel += DeltaSupply / 10;// зачисляется 10% до расчитываемого уровня
        }
    }

    private static void StatisticsUpdate()
    {
        PopulationAmountText.text = $"{(uint)_TotalAmount}";
        EducationLevelText.text = $"{Math.Round(_EducationLevel * 100,1)}%";
        MedicineLevelText.text = $"{Math.Round(_MedicineLevel * 100,1)}%";
        PopulationIncreaseRateText.text = $"{Math.Round(_BasicPopulationIncreaseRate * _TotalAmount * (-0.5f + _MedicineLevel + _NecessarySatisfactedNeeds), 2)}";
        NecessarySatisfactedNeedsText.text = $"{Math.Round(_NecessarySatisfactedNeeds * 100,1)}%";
    }
    public static void EmployablePopulationUpdate() // перерасчет доступного населения
    {
        _EmployablePopulation = _TotalAmount * (_BasePartEmployablePopulation + (0.2f - (0.4f * _NecessarySatisfactedNeeds)));
    }
    public static float DeployEmployablePopulation(uint WorkplacesAmount)
    {
        float Employees;

        if (WorkplacesAmount > _EmployablePopulation)
        {
            Employees = _EmployablePopulation;
            _EmployablePopulation -= _EmployablePopulation;
            return Employees;
        }

        if (WorkplacesAmount < _EmployablePopulation)
        {
            Employees = WorkplacesAmount;
            _EmployablePopulation -= WorkplacesAmount;
            return Employees;
        }

        return 0;
    }
    public static void SetMedicineSupply(float value) 
    {
        _MedicineSupply = value;
    }
    public static void SetEducationSupply(float value)
    {
        _EducationSupply = value;
    }

}