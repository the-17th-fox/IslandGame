using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Island : MonoBehaviour
{
    [SerializeField]
    private Text WoodAmountText;
    [SerializeField]
    private Text FoodAmountText;
    [SerializeField]
    private Text MoneyAmountText;
    [SerializeField]
    private Text PopulationAmountText;

    [SerializeField]
    private float _originalWoodAmount = 0;
    [SerializeField]
    private float _originalFoodAmount = 0;
    [SerializeField]
    private float _originalMoneyAmount = 0;
    [SerializeField]
    private float _originalPopulationAmount = 0;

    Resource Wood;
    Resource Food;
    Resource Money;
    Resource Population;

    Resource[] resources;
    Text[] statistics;

    private void Start()
    {
        Wood = gameObject.AddComponent<Resource>();
            Wood.CreateResource("Wood", _originalWoodAmount);

        Food = gameObject.AddComponent<Resource>();
            Food.CreateResource("Food", _originalFoodAmount);

        Money = gameObject.AddComponent<Resource>();
            Money.CreateResource("Money", _originalMoneyAmount, isMarketable:false);

        Population = gameObject.AddComponent<Resource>();
            Population.CreateResource("Population", _originalPopulationAmount, isMarketable:false);

        resources = new[] { Wood, Food, Money, Population };
        statistics = new[] { WoodAmountText, FoodAmountText, MoneyAmountText, PopulationAmountText };
    }

    private void Update()
    {
        Population._amount++;
        Resource.UpdateStatistics(statistics, resources);
    }
}

