using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Population : MonoBehaviour
{
    public uint _Amount;
    public float _Education;
    public float _GenerationSpeed;
    public float _Medcine;
    public float _NecessarySatisfactedNeeds;
    //public float _LuxurySatisfactedNeeds;
    public float _PopulationIncrease;
    //public float _ImmigrationAttractiveness=1;// перерасчет добавить
    private const float _NecessaryNeeds = 0.001f;
    
    [SerializeField]
    private Island island;

    private void Start()
    {

    }

    public void CreatePopulation(uint Amount,float Education,float GenerationSpeed,float Medcine) 
    {
        _Amount = Amount;
        _Education = Education;
        _GenerationSpeed = GenerationSpeed;
        _Medcine = Medcine;
        _NecessarySatisfactedNeeds = 1;  //со старта нужд нет
        //_LuxurySatisfactedNeeds = 1;
        _PopulationIncrease = 0.0002f;
    }
    public void PopulationNeedUpdate(Resource[] NessaryResources/*, Resource[] LuxuryResourse*/) 
    {
        _NecessarySatisfactedNeeds = PopulationNeed(NessaryResources);
        //_LuxurySatisfactedNeeds = PopulationNeed(LuxuryResourse);
    }
    public void PopulationIncrease() 
    {
        if (_PopulationIncrease * _Medcine * _Amount * _NecessarySatisfactedNeeds >= 0)
        {
            _Amount += (uint)(_PopulationIncrease * _Medcine * _Amount * _NecessarySatisfactedNeeds);
        }
        else 
        {
            if (_Amount < (uint)(_PopulationIncrease * _Medcine * _Amount * _NecessarySatisfactedNeeds))
            {
                _Amount = 0;
            }
            else
            {
                _Amount -= (uint)(_PopulationIncrease * _Medcine * _Amount * _NecessarySatisfactedNeeds);
            }
        }
    }
    
    public float PopulationNeed(Resource[] ConsumableResources) 
    {
        float Need = 0.001f;
        float TotalNeedSatisfation = 0;
        for(int i=0;i<ConsumableResources.Length;i++)
        {
            if (ConsumableResources[i]._amount >= Need)
            {
                TotalNeedSatisfation++;
                ConsumableResources[i]._amount -= Need;
            }
            else 
            {
                TotalNeedSatisfation += ConsumableResources[i]._amount / Need;
                ConsumableResources[i]._amount -= ConsumableResources[i]._amount / Need * Need;
            }
        }
        return (TotalNeedSatisfation / ConsumableResources.Length);
    }
    public float Needs(float needs) 
    {
        return needs * _Amount;
    }
}