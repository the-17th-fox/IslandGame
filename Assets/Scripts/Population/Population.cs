using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Population : MonoBehaviour
{
    public float _Amount;// количество населения
    public float _EducationLevel;// образованность
    public float _EducationSupply;//расчетный уровень образования
    public float _GenerationSpeed;//естественный прирост (хз вроде не юзается)
    public float _MedicineLevel;// реальный уровень медицины
    public float _MedicineSupply;//расчетный уровень медецины
    public float _NecessarySatisfactedNeeds;// удавлетворенные базовые нужды
    //public float _LuxurySatisfactedNeeds;
    public float _PopulationIncrease; // естественный прирост 
    //public float _ImmigrationAttractiveness=1;// перерасчет добавить
    private const float _NecessaryNeeds = 0.001f;// константа нужда базовых поребностей на человека
    private const float _EducationBugetPerPerson = 0.005f; // трата на образование на человека для max образованности
    private const float _MedcineBugetPerPerson = 0.01f; // трата на медицину на человека для max образованности

    [SerializeField]
    private Island island;
    private Resource[] NessosaryResources;
    //private Population population;

    private void Start()
    {
        island = gameObject.AddComponent<Island>();
        //population = gameObject.AddComponent<Population>();
        CreatePopulation(Amount: 1000, Education: 1,Medcine: 0.5f);
        NessosaryResources = new[] { island.Food };
        _MedicineSupply = 1f;
        _EducationSupply = 1f;
    }
    private void Update()
    {
        if (Timer.SecondGone())
        {
            Debug.Log($"P{_Amount} F: {island.Food._amount} M: {_MedicineLevel} Money:{island.Money._amount},E:{_EducationLevel}");
            PopulationNeedUpdate(NessosaryResources);
            MedicineUpdate();
            EducationUpdate();
            PopulationIncrease();
        }
        
    }

    public void CreatePopulation(uint Amount,float Education, float Medcine) 
    {
        _Amount = Amount;
        _EducationLevel = Education;
        _MedicineLevel = Medcine;
        _NecessarySatisfactedNeeds = 1f;  //со старта нужд нет
        //_LuxurySatisfactedNeeds = 1;
        _PopulationIncrease = 0.01f;
    }
    public void PopulationNeedUpdate(Resource[] NessaryResources/*, Resource[] LuxuryResourse*/) 
    {
        _NecessarySatisfactedNeeds = PopulationNeed(NessaryResources);
        //_LuxurySatisfactedNeeds = PopulationNeed(LuxuryResourse);
    }
    public void PopulationIncrease() 
    {
        _Amount += (_PopulationIncrease * _MedicineLevel * _Amount * _NecessarySatisfactedNeeds) * Time.deltaTime;   
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
        if (island.Money._amount >= _MedicineSupply * _MedcineBugetPerPerson * _Amount)
        {
            island.Money._amount -= _MedicineSupply * _MedcineBugetPerPerson * _Amount;//списание денег за медицину
        }
        else 
        {
            PartMedcineSupply = island.Money._amount / (_MedicineSupply * _MedcineBugetPerPerson * _Amount);
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
}