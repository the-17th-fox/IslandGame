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
    [SerializeField] private float _WorketablePopulation; // работоспособное население
    private const float _NecessaryNeeds = 0.001f;// константа нужда базовых поребностей на человека
    private const float _EducationBugetPerPerson = 0.005f; // трата на образование на человека для max образованности
    private const float _MedcineBudgetPerPerson = 0.01f; // трата на медицину на человека для max образованности
    private const float _BasePartWorketablePopulation = 0.7f; //базово работает 

    [Header("List of text fields:")]
    [SerializeField] private Text PopulationAmountText;
    [SerializeField] private Text EducationLevelText;
    [SerializeField] private Text MedicineLevelText;
    [SerializeField] private Text PopulationIncreaseRateText;
    [SerializeField] private Text NecessarySatisfactedNeedsText;

    [SerializeField] private ResourceManager islandResources;
    private ResourceManager.Resource[] _ConsumableResources;
    //private Population population;

    private void Start()
    {
        //island = gameObject.AddComponent<Island>();
        //population = gameObject.AddComponent<Population>();
        CreatePopulation(Amount: 1000, Education: 0.5f, Medcine: 0.5f);
        _ConsumableResources = new[] { islandResources.Food };
        _MedicineSupply = 1f;
        _EducationSupply = 1f;
    }
    private void Update()
    {
        if (Timer.SecondGone())
        {
            Debug.Log($"P{_Amount} F: {islandResources.Food.Amount} M: {_MedicineLevel} Money:{islandResources.Money.Amount},E:{_EducationLevel},WP:{_WorketablePopulation}");
            PopulationNeedUpdate(_ConsumableResources);
            MedicineUpdate();
            EducationUpdate();
            PopulationIncrease();
            WorketablePopulationUpdate();
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
    public void PopulationNeedUpdate(ResourceManager.Resource[] NessaryResources/*, Resource[] LuxuryResourse*/) 
    {
        _NecessarySatisfactedNeeds = PopulationNeed(NessaryResources);
        //_LuxurySatisfactedNeeds = PopulationNeed(LuxuryResourse);
    }
    public void PopulationIncrease() 
    {
        _Amount += (_BasicPopulationIncreaseRate * _Amount * (-0.5f+_MedicineLevel  + _NecessarySatisfactedNeeds)) /** Time.deltaTime*/;   
    }
    
    public float PopulationNeed(ResourceManager.Resource[] ConsumableResources) 
    {
        float NeedValue = _NecessaryNeeds * _Amount; // 1
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
    public void MedicineUpdate() 
    {
        float PartMedcineSupply = 1;
        if (islandResources.Money.Amount >= _MedicineSupply * _MedcineBudgetPerPerson * _Amount)
        {
            islandResources.Money.Amount -= _MedicineSupply * _MedcineBudgetPerPerson * _Amount;//списание денег за медицину
        }
        else 
        {
            PartMedcineSupply = islandResources.Money.Amount / (_MedicineSupply * _MedcineBudgetPerPerson * _Amount);
            islandResources.Money.Amount = 0;
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
        if (islandResources.Money.Amount >= _EducationSupply * _EducationBugetPerPerson * _Amount)
        {
            islandResources.Money.Amount -= _EducationSupply * _EducationBugetPerPerson * _Amount;//списание денег за образование
        }
        else
        {
            PartEducationSupply = islandResources.Money.Amount / (_EducationSupply * _EducationBugetPerPerson * _Amount);
            islandResources.Money.Amount = 0;
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
        PopulationIncreaseRateText.text = $"{Math.Round(_BasicPopulationIncreaseRate * _Amount * (-0.5f + _MedicineLevel + _NecessarySatisfactedNeeds), 2)}";
        NecessarySatisfactedNeedsText.text = $"{Math.Round(_NecessarySatisfactedNeeds * 100,1)}%";
    }
    public void WorketablePopulationUpdate() // перерасчет доступного населения
    {
        _WorketablePopulation = _Amount * (_BasePartWorketablePopulation + (0.2f - (0.4f * _NecessarySatisfactedNeeds)));
    }
}