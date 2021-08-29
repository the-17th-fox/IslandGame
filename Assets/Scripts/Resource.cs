using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resource : MonoBehaviour
{
    public string _name { set; get; }
    public float _amount { set; get; }
    public bool _isMarketable { set; get; }

    public void CreateResource(string Name, float Amount, bool isMarketable = true)
    {
        _name = Name; _amount = Amount; _isMarketable = isMarketable;
    }

    static public void UpdateStatistics(Text[] statistics, Resource[] resources)
    {
        for (int i = 0; i < statistics.Length; i++)
        {
            statistics[i].text = $"{resources[i]._name}: {resources[i]._amount}";
        }
    }


    //public double _amount { get; protected set; }
    //public string _name { get; protected set; }
    //public bool _isMarketable { get; protected set; }
    //static public byte _TypesOfResouse { get; protected set; } = 0;

    //public Resource (double Amount, string Name, bool isPurchasable = true)
    //{
    //    _amount = Amount; _name = Name; _isMarketable = isPurchasable; _TypesOfResouse++;
    //}
    //public void IncreaseAmount(double Value) => _amount += Value;

    //public void DecreaseAmount(double Value) => _amount -= Value;
}
