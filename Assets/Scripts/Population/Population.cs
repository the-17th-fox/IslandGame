﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Population : MonoBehaviour
{
    public float _Amount;
    public float _Education;
    public float _GenerationSpeed;
    public float _Medcine;
    public float _MedcineSupply;
    public float _NecessarySatisfactedNeeds;
    //public float _LuxurySatisfactedNeeds;
    public float _PopulationIncrease;
    //public float _ImmigrationAttractiveness=1;// перерасчет добавить
    private const float _NecessaryNeeds = 0.001f;
    private const float _MedcineBugetPerPerson = 0.01f;
    
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
        _MedcineSupply = 1f;
    }
    private void Update()
    {
        if (Timer.SecondGone())
        {
            Debug.Log($"P{_Amount} F: {island.Food._amount} M: {_Medcine} Money:{island.Money._amount}");
            PopulationNeedUpdate(NessosaryResources);
            MedcineUpdate();

            PopulationIncrease();
        }
        
    }

    public void CreatePopulation(uint Amount,float Education, float Medcine) 
    {
        _Amount = Amount;
        _Education = Education;
        _Medcine = Medcine;
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
        _Amount += (_PopulationIncrease * _Medcine * _Amount * _NecessarySatisfactedNeeds) * Time.deltaTime;   
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
    public void MedcineUpdate() // не тестировано
    {
        float PartMedcineSupply = 1;
        if (island.Money._amount >= _MedcineSupply * _MedcineBugetPerPerson * _Amount)
        {
            island.Money._amount -= _MedcineSupply * _MedcineBugetPerPerson * _Amount;//списание денег за медицину
        }
        else 
        {
            PartMedcineSupply = island.Money._amount / (_MedcineSupply * _MedcineBugetPerPerson * _Amount);
            island.Money._amount = 0;
        }

        float DeltaSupply = (PartMedcineSupply * _MedcineSupply)-_Medcine;//узнаем разницу уровня реальной медицины от оплаченной
        if (DeltaSupply < 0.03f && DeltaSupply > -0.03f)
        {
            _Medcine = PartMedcineSupply* _MedcineSupply;
        }
        else
        {
            _Medcine += DeltaSupply / 10;// зачисляется 10% до расчитываемого уровня
        }
    }
}