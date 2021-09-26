using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Population : MonoBehaviour
{
    [SerializeField] private float _Amount;// количество населения
    [SerializeField] private float _EducationLevel;// образованность
    [SerializeField] private float _EducationSupply;//расчетный уровень образования
    [SerializeField] private float _GenerationSpeed;//естественный прирост (хз вроде не юзается)
    [SerializeField] private float _MedicineLevel;// реальный уровень медицины
    [SerializeField] private float _MedicineSupply;//расчетный уровень медецины
    [SerializeField] private float _NecessarySatisfactedNeeds;// удавлетворенные базовые нужды
    //public float _LuxurySatisfactedNeeds;
    [SerializeField] private float _BasicPopulationIncreaseRate; // естественный прирост 
    //public float _ImmigrationAttractiveness=1;// перерасчет добавить
    private const float _NecessaryNeeds = 0.001f;// константа нужда базовых поребностей на человека
    private const float _EducationBugetPerPerson = 0.005f; // трата на образование на человека для max образованности
    private const float _MedcineBudgetPerPerson = 0.01f; // трата на медицину на человека для max образованности

    [Header("List of text fields:")]
    [SerializeField] private Text PopulationAmountText;
    [SerializeField] private Text EducationLevelText;
    [SerializeField] private Text MedicineLevelText;
    [SerializeField] private Text PopulationIncreaseRateText;
    [SerializeField] private Text NecessarySatisfactedNeedsText;

    [SerializeField] private Island island;
    private Resource[] _ConsumableResources;
    //private Population population;

    private void Start()
    {
        //island = gameObject.AddComponent<Island>();
        //population = gameObject.AddComponent<Population>();
        CreatePopulation(Amount: 1000, Education: 0.5f, Medcine: 0.5f);
        _ConsumableResources = new[] { island.Food };
        _MedicineSupply = 1f;
        _EducationSupply = 1f;
    }
    private void Update()
    {
        if (Timer.SecondGone())
        {
            Debug.Log($"P{_Amount} F: {island.Food._amount} M: {_MedicineLevel} Money:{island.Money._amount},E:{_EducationLevel}");
            PopulationNeedUpdate(_ConsumableResources);
            MedicineUpdate();
            EducationUpdate();
            PopulationIncrease();
        }
        StatisticsUpdate();
    }

    public void CreatePopulation(uint Amount,float Education, float Medcine) 
    {
        _Amount = Amount;
        _EducationLevel = Education;
        _MedicineLevel = Medcine;
        _NecessarySatisfactedNeeds = 1f;  //со старта нужд нет
        //_LuxurySatisfactedNeeds = 1;
        _BasicPopulationIncreaseRate = 0.01f;
    }
    public void PopulationNeedUpdate(Resource[] NessaryResources/*, Resource[] LuxuryResourse*/) 
    {
        _NecessarySatisfactedNeeds = PopulationNeed(NessaryResources);
        //_LuxurySatisfactedNeeds = PopulationNeed(LuxuryResourse);
    }
    public void PopulationIncrease() 
    {
        _Amount += (_BasicPopulationIncreaseRate * _MedicineLevel * _Amount * _NecessarySatisfactedNeeds) * Time.deltaTime;   
    }
    
    public float PopulationNeed(Resource[] ConsumableResources) 
    {
        float NeedValue = _NecessaryNeeds * _Amount; // 1
        float TotalNeedSatisfation = 0; 
        for(int i=0; i < ConsumableResources.Length; i++)
        {
            if (ConsumableResources[i]._amount >= NeedValue)
            {
                TotalNeedSatisfation++;
                ConsumableResources[i]._amount -= NeedValue * Time.deltaTime;
            }
            else 
            {
                TotalNeedSatisfation += ConsumableResources[i]._amount / NeedValue;
                ConsumableResources[i]._amount -= ConsumableResources[i]._amount * Time.deltaTime;
            }
        }
        return (TotalNeedSatisfation / ConsumableResources.Length);
    }
    public void MedicineUpdate() 
    {
        float PartMedcineSupply = 1;
        if (island.Money._amount >= _MedicineSupply * _MedcineBudgetPerPerson * _Amount)
        {
            island.Money._amount -= _MedicineSupply * _MedcineBudgetPerPerson * _Amount;//списание денег за медицину
        }
        else 
        {
            PartMedcineSupply = island.Money._amount / (_MedicineSupply * _MedcineBudgetPerPerson * _Amount);
            island.Money._amount = 0;
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
    public void EducationUpdate()
    {
        float PartEducationSupply = 1;
        if (island.Money._amount >= _EducationSupply * _EducationBugetPerPerson * _Amount)
        {
            island.Money._amount -= _EducationSupply * _EducationBugetPerPerson * _Amount;//списание денег за образование
        }
        else
        {
            PartEducationSupply = island.Money._amount / (_EducationSupply * _EducationBugetPerPerson * _Amount);
            island.Money._amount = 0;
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

    private void StatisticsUpdate()
    {
        PopulationAmountText.text = $"{(uint)_Amount}";
        EducationLevelText.text = $"{Math.Round(_EducationLevel * 100,1)}%";
        MedicineLevelText.text = $"{Math.Round(_MedicineLevel * 100,1)}%";
        PopulationIncreaseRateText.text = $"{Math.Round(_BasicPopulationIncreaseRate * _MedicineLevel * _Amount * _NecessarySatisfactedNeeds, 2)}";
        NecessarySatisfactedNeedsText.text = $"{Math.Round(_NecessarySatisfactedNeeds * 100,1)}%";
    }
}