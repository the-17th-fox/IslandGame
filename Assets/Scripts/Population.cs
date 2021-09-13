using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Population : MonoBehaviour
{

    CityPopulation cityPopulation;
    VillagesPopulation villagesPopulation;

    private void Start() 
    {
        cityPopulation = gameObject.AddComponent<CityPopulation>();
        villagesPopulation = gameObject.AddComponent<VillagesPopulation>();
            cityPopulation.CreateCityPopulation(10000, 0.5f);
            villagesPopulation.CreateVilPopulation(10000, 0.3f);
        
    }

    private void Update()
    {
        cityPopulation.PopulationGeneration();
        villagesPopulation.PopulationGeneration();
    }

    public uint _Amount;
    public float _Education;
    public float _GenerationSpeed;
    public void PopulationGeneration() 
    {
        _Amount +=(uint)( _Amount * _GenerationSpeed * Time.deltaTime);
    }
}
public class CityPopulation : Population 
{
    new public uint _Amount;
    new public float _Education;
    

    public void CreateCityPopulation(uint Amount,float Education) 
    {
        _Amount = Amount;
        _Education = Education;
    } 
}
public class VillagesPopulation : Population 
{
    new public uint _Amount;
    new public float _Education;

    public void CreateVilPopulation(uint Amount, float Education) 
    {
        _Amount = Amount;
        _Education = Education;
    }  
}
