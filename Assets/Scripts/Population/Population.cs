using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Population : MonoBehaviour
{
    [SerializeField] private static float _TotalAmount;// количество населения
    public static float TotalAmount
    {
        get => _TotalAmount;
        set
        {
            if (value < Min_Population)
            {
                _TotalAmount = Min_Population;
            }
            else
            {
                if (value >= float.MaxValue)
                {
                    _TotalAmount = float.MaxValue;
                }
                else
                { _TotalAmount = value; }
            }

        }
    }

    [SerializeField] public static float _EducationLevel;// образованность
    [SerializeField] private static float _EducationSupply;//расчетный уровень образования
    [SerializeField] private static float _GenerationSpeed;//естественный прирост (хз вроде не юзается)
    [SerializeField] public static float _MedicineLevel;// реальный уровень медицины
    [SerializeField] private static float _MedicineSupply;//расчетный уровень медецины
    [SerializeField] private static float _NecessarySatisfactedNeeds;// удавлетворенные базовые нужды
    //public static float _LuxurySatisfactedNeeds;
    [SerializeField] private static float _BasicPopulationIncreaseRate; // естественный прирост 
    //public static float _ImmigrationAttractiveness=1;// перерасчет добавить
    [HideInInspector] public static float _EmployedPopulation;
    [SerializeField] public static float _WorkablePopulation; // работоспособное население
    private const float _NecessaryNeeds = 0.1f;// константа нужда базовых поребностей на человека
    private const float _EducationBugetPerPerson = 0.005f; // трата на образование на человека для max образованности
    private const float _MedcineBudgetPerPerson = 0.01f; // трата на медицину на человека для max образованности
    private const float _BasePartEmployablePopulation = 0.7f; //базово работает 
    private const float Min_Population = 0;

    [Header("List of text fields:")]
    [SerializeField] private Text PopulationAmountText;
    [SerializeField] private Text EducationLevelText;
    [SerializeField] private Text MedicineLevelText;
    [SerializeField] private Text PopulationIncreaseRateText;
    [SerializeField] private Text NecessarySatisfactedNeedsText;
    [SerializeField] private Text UnemployedAmountText;

    private static ResourceManager.Resource[] _ConsumableResources;
    //private static Population population;

    private void Start()
    {
        //island = gameObject.AddComponent<Island>();
        //population = gameObject.AddComponent<Population>();
        CreatePopulation(Amount: 20, Education: 0.5f, Medcine: 0.5f);
        _ConsumableResources = new[] { ResourceManager.Food };
        _MedicineSupply = 1f;
        _EducationSupply = 1f;
    }
    private void Update()
    {
        if (Timer.SecondGone())
        {
            //Debug.Log($" P: {_TotalAmount} F: {ResourceManager.Food.Amount} M: {_MedicineLevel} Money:{ResourceManager.Money.Amount},E:{_EducationLevel},WP:{_UnemployedPopulation}");
            
        }
        PopulationNeedUpdate(_ConsumableResources);
        MedicineUpdate();
        EducationUpdate();
        PopulationIncrease();
        EmployablePopulationUpdate();
        Debug.Log($"UN: {(int)_WorkablePopulation} | EM: {(int)_EmployedPopulation} | FREE: {(int)(_WorkablePopulation - _EmployedPopulation)}");
        StatisticsUpdate();
    }

    private static void CreatePopulation(uint Amount, float Education, float Medcine)
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
        _NecessarySatisfactedNeeds = PopulationNeed(NessaryResources)/* * Time.deltaTime*/;
        //_LuxurySatisfactedNeeds = PopulationNeed(LuxuryResourse);
    }
    private static void PopulationIncrease()
    {
        _TotalAmount += (_BasicPopulationIncreaseRate * _TotalAmount * (-0.5f + _MedicineLevel + _NecessarySatisfactedNeeds)) * Time.deltaTime;
    }

    private static float PopulationNeed(ResourceManager.Resource[] ConsumableResources)
    {
        float NeedValue = _NecessaryNeeds * _TotalAmount; // 1
        float TotalNeedSatisfation = 0;
        for (int i = 0; i < ConsumableResources.Length; i++)
        {
            if (ConsumableResources[i].Amount >= NeedValue)
            {
                TotalNeedSatisfation++;
                ConsumableResources[i].Amount -= NeedValue * Time.deltaTime;
            }
            else
            {
                TotalNeedSatisfation += ConsumableResources[i].Amount / NeedValue;
                ConsumableResources[i].Amount -= ConsumableResources[i].Amount * Time.deltaTime;
            }
        }
        return (TotalNeedSatisfation / ConsumableResources.Length);
    }
    private static void MedicineUpdate()
    {
        float PartMedcineSupply = 1;
        if (ResourceManager.Money.Amount >= _MedicineSupply * _MedcineBudgetPerPerson * _TotalAmount)
        {
            ResourceManager.Money.Amount -= _MedicineSupply * _MedcineBudgetPerPerson * _TotalAmount * Time.deltaTime;//списание денег за медицину
        }
        else
        {
            PartMedcineSupply = ResourceManager.Money.Amount / (_MedicineSupply * _MedcineBudgetPerPerson * _TotalAmount);
            ResourceManager.Money.Amount = 0;
        }

        float DeltaSupply = (PartMedcineSupply * _MedicineSupply) - _MedicineLevel;//узнаем разницу уровня реальной медицины от оплаченной
        if (DeltaSupply < 0.03f && DeltaSupply > -0.03f)
        {
            _MedicineLevel = PartMedcineSupply * _MedicineSupply;
        }
        else
        {
            _MedicineLevel += DeltaSupply / 10 * Time.deltaTime;// зачисляется 10% до расчитываемого уровня
        }
    }
    private static void EducationUpdate()
    {
        float PartEducationSupply = 1;
        if (ResourceManager.Money.Amount >= _EducationSupply * _EducationBugetPerPerson * _TotalAmount)
        {
            ResourceManager.Money.Amount -= _EducationSupply * _EducationBugetPerPerson * _TotalAmount * Time.deltaTime;//списание денег за образование
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
            _EducationLevel += DeltaSupply / 10 * Time.deltaTime;// зачисляется 10% до расчитываемого уровня
        }
    }

    private void StatisticsUpdate()
    {
        PopulationAmountText.text = $"{(uint)_TotalAmount}";
        EducationLevelText.text = $"{Math.Round(_EducationLevel * 100, 1)}%";
        MedicineLevelText.text = $"{Math.Round(_MedicineLevel * 100, 1)}%";
        PopulationIncreaseRateText.text = $"{Math.Round(_BasicPopulationIncreaseRate * _TotalAmount * (-0.5f + _MedicineLevel + _NecessarySatisfactedNeeds), 2)}";
        NecessarySatisfactedNeedsText.text = $"{Math.Round(_NecessarySatisfactedNeeds * 100, 1)}%";
        UnemployedAmountText.text = $"{(int)_WorkablePopulation - _EmployedPopulation}";
    }
    public static void EmployablePopulationUpdate() // перерасчет доступного населения
    {
        _WorkablePopulation = _TotalAmount * (_BasePartEmployablePopulation + (0.2f - (0.4f * _NecessarySatisfactedNeeds)));
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