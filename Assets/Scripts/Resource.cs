using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Resource : MonoBehaviour
{
    public string _name { private set; get; } // название
    public float _amount { set; get; } // кол-во
    public bool _isMarketable { private set; get; } // продаваемое/покупаемо ли?
    public float _maxAmount { private set; get; } // максимальное количество
    public float _basicGenerationSpeed { private set; get; }// эффективность генерации первичных ресурсов

    public void CreateNewResource(string Name, float MaxAmount = float.MaxValue, float InitAmount = 0, float BasicGenerationSpeed = 0, bool isMarketable = true)
    {
        _name = Name;
        _maxAmount = MaxAmount;
        _amount = InitAmount;
        _basicGenerationSpeed = BasicGenerationSpeed;
        _isMarketable = isMarketable;
    }

    static public void UpdateStatistics(Text[] statistics, Resource[] resources)
    {
        if (statistics.Length != resources.Length)
        {
            Debug.LogError("UpdateStatistics : Text[] statistics and Resource[] resources don't match in the number of elements.");
            return;
        }

        for (int i = 0; i < statistics.Length; i++)
        {
            statistics[i].text = $"{Math.Round(resources[i]._amount, 1)}";
        }
    }
    public static void BasicResourcesGeneration(Resource[] resources)
    {
        foreach (Resource resource in resources)
        {
            if (resource._basicGenerationSpeed != 0 && resource._amount < resource._maxAmount)
            {
                resource._amount += resource._basicGenerationSpeed * Time.deltaTime;
            }
        }
        
    }
    
}