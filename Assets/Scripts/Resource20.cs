using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Resource : MonoBehaviour
{
    public string _name { set; get; } // название
    public float _amount { set; get; } // кол-во
    public bool _isMarketable { set; get; } // продаваемое/покупаемо ли?
    public float _basicGenerationSpeed { set; get; } // эффективность генерации первичных ресурсов

    public void CreateNewResource(string Name, float Amount, float BasicGenerationSpeed = 1, bool isMarketable = true)
    {
        _name = Name;
        _amount = Amount;
        _basicGenerationSpeed = BasicGenerationSpeed;
        _isMarketable = isMarketable;
    }

    static public void UpdateStatistics(Text[] statistics, Resource[] resources)
    {
        for (int i = 0; i < statistics.Length; i++)
        {
            statistics[i].text = $"{resources[i]._name}: {resources[i]._amount}";
        }
    }
    public void ResourceGeneration(Population VillagePopulation)
    {
        _amount += (VillagePopulation._Education * _basicGenerationSpeed * VillagePopulation._Amount / 1000) * Time.deltaTime;
    }
    public void IndusrialResourceGeneration(BasicIndustrySector industrySector, Resource[] warehouse,float EducationCityPopulation)
    {
        if (Timer.SecondGone())
        {
            for (int i = 0; i < industrySector._neededResourceID.Count; i++)
            {
                if (warehouse[industrySector._neededResourceID[i]]._amount < EducationCityPopulation*industrySector.ProductionCost())
                {
                    Debug.Log($"There isn't enought resource {warehouse[i]._name}");
                    return;
                }

            }
            for (int i = 0; i < industrySector._neededResourceID.Count; i++)
            {
                warehouse[industrySector._neededResourceID[i]]._amount -= EducationCityPopulation* industrySector.ProductionCost();
            }

            _amount +=EducationCityPopulation*industrySector.ProductionAmount();
            Debug.Log("_increase amount" + industrySector.ProductionAmount());
        }
    }
}