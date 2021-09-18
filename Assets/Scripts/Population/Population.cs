using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Population : MonoBehaviour
{
    public float _Amount;
    public float _Education;
    public float _GenerationSpeed;
    //public float _Medcine;
    public float _NecessarySatisfactedNeeds;
    //public float _LuxurySatisfactedNeeds;
    public float _PopulationIncrease;
    //public float _ImmigrationAttractiveness=1;// перерасчет добавить
    private const float _NecessaryNeeds = 0.001f;
    
    [SerializeField]
    private Island island;
    private Resource[] NessosaryResources;
    private Population population;

    private void Start()
    {
        island = gameObject.AddComponent<Island>();
        population = gameObject.AddComponent<Population>();
        population.CreatePopulation(Amount: 1000, Education: 1);
        NessosaryResources = new[] { island.Food };
    }
    private void Update()
    {
        if (Timer.SecondGone())
        {
            Debug.Log("Second gone");
            population.PopulationNeedUpdate(NessosaryResources);
            population.PopulationIncrease();
            Debug.Log("population=" + population._Amount + " Food=" + island.Food._amount);
        }
        
    }

    public void CreatePopulation(uint Amount,float Education/*,float Medcine*/) 
    {
        _Amount = Amount;
        _Education = Education;
        //_Medcine = Medcine;
        _NecessarySatisfactedNeeds = 1;  //со старта нужд нет
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
     _Amount += (_PopulationIncrease */*_Medcine* */ _Amount * _NecessarySatisfactedNeeds);   
    }
    
    public float PopulationNeed(Resource[] ConsumableResources) 
    {
        float NeedValue = _NecessaryNeeds * _Amount;
        float TotalNeedSatisfation = 0;
        for(int i=0; i < ConsumableResources.Length; i++)
        {
            if (ConsumableResources[i]._amount >= NeedValue)
            {
                TotalNeedSatisfation++;
                ConsumableResources[i]._amount -= NeedValue;
            }
            else 
            {
                TotalNeedSatisfation += ConsumableResources[i]._amount / NeedValue;
                ConsumableResources[i]._amount -= ConsumableResources[i]._amount;
            }
        }
        return (TotalNeedSatisfation / ConsumableResources.Length);
    }
}