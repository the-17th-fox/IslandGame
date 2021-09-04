//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class Resource : MonoBehaviour
//{
//    public string _name { set; get; }
//    public float _amount { set; get; }
//    public bool _isMarketable { set; get; }
//    public float _basicGenerationSpeed { set; get; }

//    public void CreateNewResource(string Name, float Amount, float BasicGenerationSpeed = 1, bool isMarketable = true)
//    {
//        _name = Name;
//        _amount = Amount;
//        _basicGenerationSpeed = BasicGenerationSpeed;
//        _isMarketable = isMarketable;
//    }

//    static public void UpdateStatistics(Text[] statistics, Resource[] resources)
//    {
//        for (int i = 0; i < statistics.Length; i++)
//        {
//            statistics[i].text = $"{resources[i]._name}: {resources[i]._amount}";
//        }
//    }
//    public void ResourceGeneration(uint Employees)
//    {
//        _amount += (_basicGenerationSpeed * Employees / 1000) * Time.deltaTime;
//    }
//}