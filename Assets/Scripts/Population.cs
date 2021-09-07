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
    }

    private void Update()
    {
        cityPopulation.CreateCityPopulation();
        villagesPopulation.CreateVilPopulation();
    }

    public uint _Amount;
}
public class CityPopulation : Population 
{
    new public uint _Amount;

    public void CreateCityPopulation() 
    {
        _Amount = 10;
    } 
}
public class VillagesPopulation : Population 
{
    new public uint _Amount;

    public void CreateVilPopulation() 
    {
        _Amount = 5;
    }  
}
